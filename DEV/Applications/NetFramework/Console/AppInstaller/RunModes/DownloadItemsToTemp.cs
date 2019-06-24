using System.Collections.Generic;
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
    public class DownloadItemsToTemp
    {
        private readonly string appInstallerAssemblyName;
        private readonly IDirectoryService directoryService;

        public DownloadItemsToTemp(
            string appInstallerAssemblyName,
            IDirectoryService directoryService = null)
        {
            this.appInstallerAssemblyName = appInstallerAssemblyName;
            this.directoryService = directoryService.Resolve();
        }
        
        public AppInstallerResult Run(
            string sourceDir, 
            string installDir, 
            string originalAppPath,
            List<string> excludeRegex)
        {
            var tempDirectoryPath = new DirectoryInfo(Path.Combine(installDir, "..", "AppInstaller_Temp")).FullName;
            var excludeRegexList = excludeRegex.Select(ex => new Regex(ex));

            var excludeRelativePaths = this.directoryService.GetFiles(sourceDir, "*", SearchOption.AllDirectories)
                .Where(f => !excludeRegexList.AnyIsMatch(f.GetRelativePath(sourceDir))).ToHashSet();

            sourceDir.CopyDirectory(
                tempDirectoryPath,
                true,
                true,
                excludeRelativePaths);

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