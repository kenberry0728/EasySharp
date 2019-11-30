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
            #pragma warning disable CA1062 // Validate arguments of public methods
            argument.ThrowExceptionIfNull(nameof(argument));
            var appInstallerPathInTempDir = Path.Combine(argument.TempFolder, this.appInstallerAssemblyName);
            #pragma warning restore CA1062 // Validate arguments of public methods

            var appInstallerInInstallDir = appInstallerPathInTempDir.GetProcessByFileName();
            appInstallerInInstallDir?.WaitForExit(5000);

            Try.To(() => Directory.Delete(argument.TempFolder, true));
            argument.OriginalAppPath?.RunProcess();
            appInstallerInInstallDir.Dispose();
            return new AppInstallerResult { ResultCode = ResultCode.Success, Updated = true };
        }
    }
}