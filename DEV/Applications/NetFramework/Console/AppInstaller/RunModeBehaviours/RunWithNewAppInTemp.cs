using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AppInstaller.Core.Arguments;
using AppInstaller.Core.Results;
using EasySharpStandard.DiskIO;
using EasySharpStandard.Processes;

namespace AppInstaller.RunModeBehaviours
{
    public class RunWithNewAppInTemp
    {
        private readonly string appInstallerAssemblyName;

        public RunWithNewAppInTemp(string appInstallerAssemblyName)
        {
            this.appInstallerAssemblyName = appInstallerAssemblyName;
        }

        public AppInstallerResult Run(AppInstallerArgument appInstallerArgument)
        {
            WaitForExitInInstallDir(appInstallerArgument);
            var regex = appInstallerArgument.ExcludePathRegex.Select(reg => new Regex(reg)).ToList();
            appInstallerArgument.SourceDir.CopyDirectory(
                appInstallerArgument.InstallDir,
                true,
                true,
                f => f.IsTargetFile(appInstallerArgument.SourceDir, regex));

            var newInstallerPath = Path.Combine(appInstallerArgument.InstallDir, appInstallerAssemblyName);
            appInstallerArgument.RunMode = RunMode.CleanupAndRunApp;
            newInstallerPath.RunProcess(appInstallerArgument.ToCommandLineString());

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