﻿using System.IO;
using AppInstaller.Core.Arguments;
using AppInstaller.Core.Results;
using EasySharp.IO;
using EasySharp;
using EasySharp.Diagnostics;

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
            #pragma warning disable CA1062 // Validate arguments of public methods
            argument.ThrowArgumentExceptionIfNull(nameof(argument));
            var installDir = argument.InstallDir.ToDirectoryPath().ToFullDirectoryPath().Value;
            #pragma warning restore CA1062 // Validate arguments of public methods

            var sourceDir = new DirectoryInfo(argument.SourceDir).FullName;
            var tempDirectoryPath = Path.Combine(installDir, "..", "AppInstaller_Temp").ToDirectoryPath().ToFullDirectoryPath().Value;

            CopyAppInstallerFiles(sourceDir, tempDirectoryPath);

            var appInstallerForUpdatePath = Path.Combine(tempDirectoryPath, appInstallerAssemblyName);
            var runAppInstallerInTempArg = CreateRunAppInstallerInTempArgument(argument, tempDirectoryPath);
            appInstallerForUpdatePath.ToFilePath().RunProcess(runAppInstallerInTempArg.ToCommandLineString(), true);

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
            var sourceFileName = Path.Combine(sourceDir, appFilesTxt).ToFilePath();
            var destFileName = Path.Combine(tempDirectoryPath, appFilesTxt).ToFilePath();
            sourceFileName.Copy(destFileName);
            var fileNames = destFileName.ReadLines(true);
            foreach (var fileName in fileNames)
            {
                var sourceFile = Path.Combine(sourceDir, fileName).ToFilePath();
                var destFile = Path.Combine(tempDirectoryPath, fileName).ToFilePath();
                sourceFile.Copy(destFile);
            }
        }
    }
}