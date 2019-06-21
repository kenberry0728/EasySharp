using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AppInstaller.Core.Arguments;
using AppInstaller.Core.Results;
using EasySharpStandard.DiskIO;
using EasySharpStandard.Processes;

namespace AppInstaller.RunModes
{
    public class DownloadItemsToTemp
    {
        private readonly string appInstallerAssemblyName;

        public DownloadItemsToTemp(string appInstallerAssemblyName)
        {
            this.appInstallerAssemblyName = appInstallerAssemblyName;
        }

        public AppInstallerResult Run(AppInstallerArgument appInstallerArgument)
        {
            var tempDirectoryPath = new DirectoryInfo(Path.Combine(appInstallerArgument.InstallDir, "..", "AppInstaller_Temp")).FullName;
            var excludeRegexList = appInstallerArgument.ExcludePathRegex.Select(ex => new Regex(ex));
            appInstallerArgument.SourceDir.CopyDirectory(
                tempDirectoryPath,
                true,
                true,
                f => f.IsTargetFile(tempDirectoryPath, excludeRegexList));
            var appInstallerForUpdatePath = Path.Combine(tempDirectoryPath, appInstallerAssemblyName);
            appInstallerArgument.TempFolder = tempDirectoryPath;
            appInstallerArgument.RunMode = RunMode.RunWithNewAppInTemp;
            appInstallerForUpdatePath.RunProcess(appInstallerArgument.ToCommandLineString());
            return new AppInstallerResult { ResultCode = ResultCode.Success };
        }

    }
}