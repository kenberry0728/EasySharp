using System;
using System.IO;
using AppInstaller.Core.Arguments;
using AppInstaller.Core.Results;
using EasySharpStandard.DiskIO;
using EasySharpStandard.DiskIO.Serializers;
using EasySharpStandard.Processes;

namespace AppInstaller.RunModes
{
    public class DownloadAppInstallerToTempAndRun
    {
        private readonly string appInstallerAssemblyName;

        public DownloadAppInstallerToTempAndRun(
            string appInstallerAssemblyName)
        {
            this.appInstallerAssemblyName = appInstallerAssemblyName;
        }
        
        public AppInstallerResult Run(AppInstallerArgument argument)
        {
            var installDir = argument.InstallDir.ToFullDirectoryName();
            var sourceDir = new DirectoryInfo(argument.SourceDir).FullName;
            var tempDirectoryPath = Path.Combine(installDir, "..", "AppInstaller_Temp").ToFullDirectoryName();

            CopyAppInstallerFiles(sourceDir, tempDirectoryPath);

            var appInstallerForUpdatePath = Path.Combine(tempDirectoryPath, appInstallerAssemblyName);
            var runAppInstallerInTempArg = CreateRunAppInstallerInTempArgument(argument, tempDirectoryPath);
            appInstallerForUpdatePath.RunProcess(runAppInstallerInTempArg.ToCommandLineString(), true);

            return new AppInstallerResult {ResultCode = ResultCode.Success};
        }

        private static AppInstallerArgument CreateRunAppInstallerInTempArgument(
            AppInstallerArgument argument,
            string tempDirectoryPath)
        {
            var newArgument = argument.Clone();
            newArgument.RunMode = RunMode.RunNewAppInstallerInTempFolder;
            newArgument.TempFolder = tempDirectoryPath;
            return newArgument;
        }

        private static void CopyAppInstallerFiles(string sourceDir, string tempDirectoryPath)
        {
            const string appFilesTxt = "AppInstallerFiles.txt";
            var sourceFileName = Path.Combine(sourceDir, appFilesTxt);
            var destFileName = Path.Combine(tempDirectoryPath, appFilesTxt);
            destFileName.EnsureDirectoryForFile();
            File.Copy(sourceFileName, destFileName, true);
            var fileNames = destFileName.ReadLines(true);
            foreach (var fileName in fileNames)
            {
                var sourceFile = Path.Combine(sourceDir, fileName);
                var destFile = Path.Combine(tempDirectoryPath, fileName);
                File.Copy(sourceFile, destFile, true);
            }
        }
    }
}