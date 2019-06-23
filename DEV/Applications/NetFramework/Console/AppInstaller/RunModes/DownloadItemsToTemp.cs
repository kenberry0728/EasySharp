using System.Collections.Generic;
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
        
        public AppInstallerResult Run(
            string sourceDir, 
            string installDir, 
            string originalAppPath,
            List<string> excludeRegex)
        {
            var tempDirectoryPath = new DirectoryInfo(Path.Combine(installDir, "..", "AppInstaller_Temp")).FullName;
            var excludeRegexList = excludeRegex.Select(ex => new Regex(ex));
            sourceDir.CopyDirectory(
                tempDirectoryPath,
                true,
                true,
                f => f.FullName.IsTargetFile(tempDirectoryPath, excludeRegexList));
            var appInstallerForUpdatePath = Path.Combine(tempDirectoryPath, appInstallerAssemblyName);
            var argument = new AppInstallerArgument(
                RunMode.RunWithNewAppInTemp)
            {
                ExcludePathRegex = excludeRegex,
                InstallDir = installDir,
                OriginalAppPath = originalAppPath,
                SourceDir = sourceDir,
                TempFolder = tempDirectoryPath
            };

            var process = appInstallerForUpdatePath.RunProcess(argument.ToCommandLineString());
            var result = process.WaitForExit(10000);
            return result
                ? new AppInstallerResult { ResultCode = ResultCode.Success } 
                : new AppInstallerResult { ResultCode = ResultCode.Fail };
        }
    }
}