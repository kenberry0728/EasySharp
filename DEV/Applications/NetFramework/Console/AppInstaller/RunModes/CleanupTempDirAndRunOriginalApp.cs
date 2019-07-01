using System.IO;
using AppInstaller.Core.Arguments;
using AppInstaller.Core.Results;
using EasySharpStandard.Processes;

namespace AppInstaller.RunModes
{
    public class CleanupTempDirAndRunOriginalApp
    {
        private readonly string appInstallerAssemblyName;

        public CleanupTempDirAndRunOriginalApp(string appInstallerAssemblyName)
        {
            this.appInstallerAssemblyName = appInstallerAssemblyName;
        }

        public AppInstallerResult Run(AppInstallerArgument argument)
        {
            var appInstallerPathInInstallDir = Path.Combine(argument.TempFolder, appInstallerAssemblyName);
            var appInstallerInInstallDir = appInstallerPathInInstallDir.GetProcessByFileName();
            appInstallerInInstallDir?.WaitForExit(5000);

            Directory.Delete(argument.TempFolder, true);
            argument.OriginalAppPath?.RunProcess();
            return new AppInstallerResult { ResultCode = ResultCode.Success, Updated = true };
        }
    }
}