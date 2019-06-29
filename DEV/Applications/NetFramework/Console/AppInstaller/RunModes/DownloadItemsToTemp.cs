using System;
using System.IO;
using AppInstaller.Core.Arguments;
using AppInstaller.Core.Results;
using EasySharpStandard.DiskIO;
using EasySharpStandard.DiskIO.Serializers;
using EasySharpStandard.Processes;

namespace AppInstaller.RunModes
{
    public class DownloadItemsToTemp
    {
        private readonly string appInstallerAssemblyName;

        public DownloadItemsToTemp(
            string appInstallerAssemblyName)
        {
            this.appInstallerAssemblyName = appInstallerAssemblyName;
        }
        
        public AppInstallerResult Run(AppInstallerArgument argument)
        {
            var installDir = argument.InstallDir;
            var sourceDir = argument.SourceDir;

            var tempDirectoryPath = new DirectoryInfo(Path.Combine(installDir, "..", "AppInstaller_Temp")).FullName;
            tempDirectoryPath.EnsureDirectoryForFile();

            CopyAppInstallerFiles(sourceDir, tempDirectoryPath);

            var appInstallerForUpdatePath = Path.Combine(tempDirectoryPath, appInstallerAssemblyName);
            var newArgument = argument.Clone();
            newArgument.RunMode = RunMode.RunWithNewAppInTemp;

            var process = appInstallerForUpdatePath.RunProcess(argument.ToCommandLineString());
            var result = process.WaitForExit(10000);
            return result
                ? new AppInstallerResult { ResultCode = ResultCode.Success } 
                : new AppInstallerResult { ResultCode = ResultCode.Fail };
        }

        private static void CopyAppInstallerFiles(string sourceDir, string tempDirectoryPath)
        {
            const string appFilesTxt = "AppFiles.txt";
            var sourceFileName = Path.Combine(sourceDir, appFilesTxt);
            var destFileName = Path.Combine(tempDirectoryPath, appFilesTxt);
            File.Copy(sourceFileName, destFileName, true);
            var fileNames = destFileName.ReadLines(StringSplitOptions.RemoveEmptyEntries);
            foreach (var fileName in fileNames)
            {
                var sourceFile = Path.Combine(sourceDir, fileName);
                var destFile = Path.Combine(tempDirectoryPath, fileName);
                File.Copy(sourceFile, destFile, true);
            }
        }
    }
}