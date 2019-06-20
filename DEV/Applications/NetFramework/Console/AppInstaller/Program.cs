using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using AppInstaller.Core.Arguments;
using AppInstaller.Core.Results;
using AppInstaller.Implementation;
using EasySharpStandard.DiskIO;
using EasySharpStandard.DiskIO.Serializers;
using EasySharpStandard.Processes;

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
                    return CheckUpdate(argument);
                case RunMode.DownloadItemsToTemp:
                    return DownloadItemsToTemp(argument);
                case RunMode.RunWithNewAppInTemp:
                    return new RunWithNewAppInTemp(appInstallerAssemblyName).Run(argument);
                case RunMode.CleanupAndRunApp:
                    return new CleanupAndRunApp(appInstallerAssemblyName).Run(argument);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static AppInstallerResult CheckUpdate(AppInstallerArgument appInstallerArgument)
        {
            var excludeRegexList = appInstallerArgument.ExcludePathRegex.Select(ex => new Regex(ex)).ToList();

            var sourceDirInfo = new DirectoryInfo(appInstallerArgument.SourceDir);
            var sourceLastUpdateDate = GetLastWriteTimeUtc(sourceDirInfo, excludeRegexList);

            var installDirInfo = new DirectoryInfo(appInstallerArgument.InstallDir);
            var installLastUpdateDate = GetLastWriteTimeUtc(installDirInfo, excludeRegexList);
            return new AppInstallerResult
            {
                ResultCode = ResultCode.Success,
                Updated = sourceLastUpdateDate > installLastUpdateDate
            };

        }

        private static DateTime GetLastWriteTimeUtc(DirectoryInfo targetDirectoryInfo, IEnumerable<Regex> regex)
        {
            return targetDirectoryInfo.GetFiles("*", SearchOption.AllDirectories)
                .Where(f => f.IsTargetFile(targetDirectoryInfo.FullName, regex))
                .Max(f => f.LastWriteTimeUtc);
        }

        private static AppInstallerResult DownloadItemsToTemp(AppInstallerArgument appInstallerArgument)
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
            return new AppInstallerResult {ResultCode = ResultCode.Success};
        }
    }
}
