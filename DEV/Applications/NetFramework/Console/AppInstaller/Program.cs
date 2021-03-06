﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using AppInstaller.Core.Arguments;
using AppInstaller.Core.Results;
using AppInstaller.Implementation;
using AppInstaller.RunModes;
using EasySharp.IO;
using EasySharp;

namespace AppInstaller
{
    internal class Program
    {
        private static readonly string appInstallerAssemblyName = Path.GetFileName(Assembly.GetExecutingAssembly().Location);

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
#if DEBUG
                var appArg = new AppInstallerArgument(RunMode.RunExistingAppInstallerInAppFolder)
                {
                    SourceDir = @"..\New",
                    InstallDir = @"..\Old",
                    OriginalAppPath = @"Updated.txt",
                    TempFolder = @"..\AppInstaller_Temp",
                };

                appArg.ExcludeRelativePathRegex.Add(@".*\.log");

                args = new[] { appArg.ToCommandLineString(false) };
#else
                return;
#endif
            }

            AppInstallerResult modeAppInstallerResult;
            try
            {
                modeAppInstallerResult = InternalMain(args[0]);
            }
            #pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
            #pragma warning restore CA1031 // Do not catch general exception types
            {
                modeAppInstallerResult = new AppInstallerResult
                {
                    ResultCode = ResultCode.InternalError,
                    Message = e.Message
                };

                var message = string.Join(Environment.NewLine,
                    e.Message,
                    args[0]);
                "Error.log".ToFilePath().WriteAllText(message);
            }

            Console.WriteLine(new AppInstallerResultConverter().ToString(modeAppInstallerResult));
        }

        private static AppInstallerResult InternalMain(string arg)
        {
            var result = Try.To(() => new AppInstallerArgumentConverter().FromString(arg));
            if (!result.Ok)
            {
                throw new InvalidEnumArgumentException("Invalid for enum");
            }

            var argument = result.Value;
            argument.TempFolder = argument.TempFolder.ToDirectoryPath().ToFullDirectoryPath().Value;
            argument.SourceDir = argument.SourceDir.ToDirectoryPath().ToFullDirectoryPath().Value;
            argument.InstallDir = argument.InstallDir.ToDirectoryPath().ToFullDirectoryPath().Value;
            argument.OriginalAppPath = argument.OriginalAppPath.ToFilePath().ToFullPath().Value;

            switch (argument.RunMode)
            {
                case RunMode.CheckUpdate:
                    return new CheckUpdate().Run(
                        argument.SourceDir,
                        argument.InstallDir,
                        argument.ExcludeRelativePathRegex);

                case RunMode.RunExistingAppInstallerInAppFolder:
                    Console.WriteLine("Downloading app installer to the temp folder.");
                    return new DownloadAppInstallerToTempAndRun(appInstallerAssemblyName).Run(argument);
                case RunMode.RunNewAppInstallerInTempFolder:
                    Console.WriteLine("Updating app folder.");
                    return new CopyItemsToInstallFolderAndRun(appInstallerAssemblyName).Run(argument);
                case RunMode.RunNewAppInstallerInAppFolder:
                    Console.WriteLine("Cleaning up the temp folder.");
                    return new CleanupTempDirAndRunOriginalApp(appInstallerAssemblyName).Run(argument);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
