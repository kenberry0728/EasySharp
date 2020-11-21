using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AppInstaller.Core.Arguments;
using AppInstaller.Core.Results;
using EasySharp;
using EasySharp.IO;
using EasySharp.Processes;
using EasySharp.Text.RegularExpressions;
using static EasySharp.IO.DirectoryPath;

namespace AppInstaller.RunModes
{
    public class CopyItemsToInstallFolderAndRun
    {
        private readonly string appInstallerAssemblyName;

        public CopyItemsToInstallFolderAndRun(
            string appInstallerAssemblyName)
        {
            this.appInstallerAssemblyName = appInstallerAssemblyName;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Checked")]
        public AppInstallerResult Run(AppInstallerArgument appInstallerArgument)
        {
            appInstallerArgument.ThrowArgumentExceptionIfNull(nameof(appInstallerArgument));
            WaitForExitInInstallDir(appInstallerArgument);

            var excludeRelativePathRegex = appInstallerArgument
                .ExcludeRelativePathRegex
                .Select(reg => new Regex(reg, RegexOptions.IgnoreCase))
                .ToList();
            var directoryPath = appInstallerArgument.SourceDir.ToDirectoryPath();
            var allFiles = directoryPath.GetFiles(
                "*",
                SearchOption.AllDirectories);
            var excludeRelativePaths = allFiles
                .Select(f => f.ToFilePath().GetRelativePath(appInstallerArgument.SourceDir.ToDirectoryPath()).Value)
                .Where(f => excludeRelativePathRegex.AnyIsMatch(f))
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            appInstallerArgument.SourceDir.ToDirectoryPath().CopyDirectory(
                appInstallerArgument.InstallDir.ToDirectoryPath(),
                true,
                true, 
                excludeRelativePaths);

            var newInstallerPath = Path.Combine(appInstallerArgument.InstallDir, appInstallerAssemblyName);
            var newArgument = CreateRunNewAppInstallerInAppFolderArgument(appInstallerArgument);
            newInstallerPath.RunProcess(newArgument.ToCommandLineString(), true);

            return new AppInstallerResult { ResultCode = ResultCode.Success, Updated = true };
        }

        private static AppInstallerArgument CreateRunNewAppInstallerInAppFolderArgument(AppInstallerArgument appInstallerArgument)
        {
            var newArgument = appInstallerArgument.Clone();
            newArgument.RunMode = RunMode.RunNewAppInstallerInAppFolder;
            return newArgument;
        }

        private void WaitForExitInInstallDir(AppInstallerArgument appInstallerArgument)
        {
            if (!appInstallerArgument.OriginalAppPath.IsNullOrEmpty())
            {
                var originalApp = appInstallerArgument.OriginalAppPath.ToFilePath().GetProcessByFileName();
                originalApp.Value?.WaitForExit(100000);
                originalApp.Value?.Dispose();
            }

            var appInstallerPathInInstallDir = Path.Combine(appInstallerArgument.InstallDir, appInstallerAssemblyName);
            var appInstallerInInstallDir = appInstallerPathInInstallDir.ToFilePath().GetProcessByFileName();
            appInstallerInInstallDir.Value?.WaitForExit(100000);
            appInstallerInInstallDir.Value?.Dispose();
        }
    }
}