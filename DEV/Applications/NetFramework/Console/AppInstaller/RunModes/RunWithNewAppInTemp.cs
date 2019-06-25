using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AppInstaller.Core.Arguments;
using AppInstaller.Core.Results;
using EasySharpStandard.DiskIO;
using EasySharpStandard.DiskIO.Directories.Core;
using EasySharpStandard.DiskIO.Directories.Implementation;
using EasySharpStandard.Processes;
using EasySharpStandard.RegularExpressions.Core;

namespace AppInstaller.RunModes
{
    public class RunWithNewAppInTemp
    {
        private readonly string appInstallerAssemblyName;
        private readonly IDirectoryService directoryService;

        public RunWithNewAppInTemp(
            string appInstallerAssemblyName,
            IDirectoryService directoryService = null)
        {
            this.appInstallerAssemblyName = appInstallerAssemblyName;
            this.directoryService = directoryService.Resolve();
        }

        public AppInstallerResult Run(AppInstallerArgument appInstallerArgument)
        {
            WaitForExitInInstallDir(appInstallerArgument);

            var excludeRegexList = appInstallerArgument.ExcludePathRegex.Select(reg => new Regex(reg)).ToList();
            var allFiles = this.directoryService.GetFiles(appInstallerArgument.SourceDir, "*", SearchOption.AllDirectories);
            var excludeRelativePaths = allFiles.Where(
                f => 
                    !excludeRegexList.AnyIsMatch(f.GetRelativePath(appInstallerArgument.SourceDir)))
                .ToHashSet();

            appInstallerArgument.SourceDir.CopyDirectory(
                appInstallerArgument.InstallDir,
                true,
                true, 
                excludeRelativePaths);

            var newInstallerPath = Path.Combine(appInstallerArgument.InstallDir, appInstallerAssemblyName);
            var newArgument = appInstallerArgument.Clone();
            newArgument.RunMode = RunMode.CleanupAndRunApp;
            newInstallerPath.RunProcess(newArgument.ToCommandLineString());

            return new AppInstallerResult { ResultCode = ResultCode.Success, Updated = true };
        }

        private void WaitForExitInInstallDir(AppInstallerArgument appInstallerArgument)
        {
            if (!string.IsNullOrEmpty(appInstallerArgument.OriginalAppPath))
            {
                var originalApp = appInstallerArgument.OriginalAppPath.GetProcessByFileName();
                originalApp?.WaitForExit();
            }

            var appInstallerPathInInstallDir = Path.Combine(appInstallerArgument.InstallDir, appInstallerAssemblyName);
            var appInstallerInInstallDir = appInstallerPathInInstallDir.GetProcessByFileName();
            appInstallerInInstallDir?.WaitForExit(10000);
        }
    }
}