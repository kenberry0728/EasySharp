using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using AppInstaller.Core.Arguments;
using AppInstaller.Core.Results;
using AppInstaller.Implementation;
using AppInstaller.RunModes;
using EasySharpStandard.DiskIO;
using EasySharpStandard.DiskIO.Serializers;
using EasySharpStandard.SafeCodes.Core;

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
                    TempFolder = @"..\AppInstaller_Temp"
                };

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
            catch (Exception e)
            {
                modeAppInstallerResult = new AppInstallerResult
                {
                    ResultCode = ResultCode.InternalError,
                    Message = e.Message
                };

                string.Join(Environment.NewLine,
                    e.Message,
                    args[0]).WriteToFile("Error.log");
            }

            Console.WriteLine(new AppInstallerResultConverter().ToString(modeAppInstallerResult));
        }

        private static AppInstallerResult InternalMain(string arg)
        {
            var result = Try.To(() => new AppInstallerArgumentConverter().FromString(arg), out var argument);
            if (!result)
            {
                throw new InvalidEnumArgumentException();
            }

            argument.TempFolder = argument.TempFolder.ToFullDirectoryName();
            argument.SourceDir = argument.SourceDir.ToFullDirectoryName();
            argument.InstallDir = argument.InstallDir.ToFullDirectoryName();
            argument.OriginalAppPath = argument.OriginalAppPath.ToFullFileName();

            switch (argument.RunMode)
            {
                case RunMode.CheckUpdate:
                    return new CheckUpdate().Run(
                        argument.SourceDir,
                        argument.InstallDir,
                        argument.ExcludeRelativePathRegex);

                case RunMode.RunExistingAppInstallerInAppFolder:
                    return new DownloadAppInstallerToTempAndRun(appInstallerAssemblyName).Run(argument);
                case RunMode.RunNewAppInstallerInTempFolder:
                    return new CopyItemsToInstallFolderAndRun(appInstallerAssemblyName).Run(argument);
                case RunMode.RunNewAppInstallerInAppFolder:
                    return new CleanupTempDirAndRunOriginalApp(appInstallerAssemblyName).Run(argument);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
