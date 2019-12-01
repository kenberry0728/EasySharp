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
        private readonly CreateDirectoryPath createDirectoryPath;

        public CopyItemsToInstallFolderAndRun(
            string appInstallerAssemblyName,
            CreateDirectoryPath createDirectoryPath  = null)
        {
            this.appInstallerAssemblyName = appInstallerAssemblyName;
            this.createDirectoryPath = createDirectoryPath ?? Create;
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
            var directoryPath = this.createDirectoryPath(appInstallerArgument.SourceDir);
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
                var originalApp = appInstallerArgument.OriginalAppPath.GetProcessByFileName();
                originalApp?.WaitForExit(100000);
                originalApp.Dispose();
            }

            var appInstallerPathInInstallDir = Path.Combine(appInstallerArgument.InstallDir, appInstallerAssemblyName);
            var appInstallerInInstallDir = appInstallerPathInInstallDir.GetProcessByFileName();
            appInstallerInInstallDir?.WaitForExit(100000);
            appInstallerInInstallDir.Dispose();
        }
    }
}