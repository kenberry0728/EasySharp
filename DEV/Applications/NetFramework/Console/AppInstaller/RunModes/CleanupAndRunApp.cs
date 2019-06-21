using System.IO;
using AppInstaller.Core.Arguments;
using AppInstaller.Core.Results;
using EasySharpStandard.Processes;

namespace AppInstaller.RunModes
{
    public class CleanupAndRunApp
    {
        private readonly string appInstallerAssemblyName;

        public CleanupAndRunApp(string appInstallerAssemblyName)
        {
            this.appInstallerAssemblyName = appInstallerAssemblyName;
        }

        public AppInstallerResult Run(AppInstallerArgument argument)
        {
            var appInstallerPathInInstallDir = Path.Combine(argument.TempFolder, appInstallerAssemblyName);
            var appInstallerInInstallDir = appInstallerPathInInstallDir.GetProcessByFileName();
            appInstallerInInstallDir?.WaitForExit(10000);

            Directory.Delete(argument.TempFolder, true);
            argument.OriginalAppPath?.RunProcess();
            return new AppInstallerResult { ResultCode = ResultCode.Success, Updated = true };
        }

    }
}