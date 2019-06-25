using System;
using System.IO;
using System.Linq;
using System.Reflection;
using AppInstaller.Core.Arguments;
using AppInstaller.Core.Results;
using AppInstaller.Implementation;
using AppInstaller.RunModes;
using EasySharpStandard.DiskIO.Serializers;

namespace AppInstaller
{
    internal class Program
    {
        private static readonly string appInstallerAssemblyName = Path.GetFileName(Assembly.GetExecutingAssembly().Location);

        static void Main(string[] args)
        {
            #if DEBUG
            var arg = new AppInstallerArgument(RunMode.DownloadItemsToTemp) { SourceDir = @"c:\testDir"};
            var commandLineArg = new AppInstallerArgumentConverter().ToCommandLineString(arg);
            #endif

            AppInstallerResult modeAppInstallerResult;
            try
            {
                modeAppInstallerResult = InternalMain(args);
            }
            catch (Exception e)
            {
                modeAppInstallerResult = new AppInstallerResult
                {
                    ResultCode = ResultCode.InternalError,
                    Message = e.Message
                };

                e.Message.WriteToFile("Error.log");
            }

            Console.WriteLine(new AppInstallerResultConverter().ToString(modeAppInstallerResult));
        }

        private static AppInstallerResult InternalMain(string[] args)
        {
            var arg = args.Any() ? args[0] : "AppInstaller_UpdateArg.json".ReadToEnd();
            var argument = new AppInstallerArgumentConverter().FromString(arg);

#if DEBUG
            if (args.Any() && argument.RunMode != RunMode.CheckUpdate)
            {
                args[0].WriteToFile(argument.RunMode + ".txt");
            }
#endif

            switch (argument.RunMode)
            {
                case RunMode.CheckUpdate:
                    return new CheckUpdate().Run(
                        argument.SourceDir,
                        argument.InstallDir,
                        argument.ExcludePathRegex);
                case RunMode.DownloadItemsToTemp:
                    return new DownloadItemsToTemp(appInstallerAssemblyName).Run(argument);
                case RunMode.RunWithNewAppInTemp:
                    return new RunWithNewAppInTemp(appInstallerAssemblyName).Run(argument);
                case RunMode.CleanupAndRunApp:
                    return new CleanupAndRunApp(appInstallerAssemblyName).Run(argument);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
