using System.IO;
using AppInstaller.Core.Arguments;
using AppInstaller.Core.Results;
using EasySharp;
using EasySharp.Processes;

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
            var appInstallerPathInTempDir = Path.Combine(argument.TempFolder, appInstallerAssemblyName);
            var appInstallerInInstallDir = appInstallerPathInTempDir.GetProcessByFileName();
            appInstallerInInstallDir?.WaitForExit(5000);

            Try.To(() => Directory.Delete(argument.TempFolder, true));
            argument.OriginalAppPath?.RunProcess();
            return new AppInstallerResult { ResultCode = ResultCode.Success, Updated = true };
        }
    }
}