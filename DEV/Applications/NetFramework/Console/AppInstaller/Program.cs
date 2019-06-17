using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
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
                    return RunWithNewAppInTemp(argument);
                case RunMode.CleanupAndRunApp:
                    return CleanupAndRunApp(argument);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static AppInstallerResult CheckUpdate(AppInstallerArgument appInstallerArgument)
        {
            var sourceDirInfo = new DirectoryInfo(appInstallerArgument.SourceDir);
            var latestUpdateDate = sourceDirInfo.GetFiles("*", SearchOption.AllDirectories)
                .Max(f => f.LastWriteTimeUtc);

            var installDirInfo = new DirectoryInfo(appInstallerArgument.InstallDir);
            var currentUpdateDate = installDirInfo.GetFiles("*", SearchOption.AllDirectories)
                .Max(f => f.LastWriteTimeUtc);
            return new AppInstallerResult
            {
                ResultCode = ResultCode.Success,
                Updated = latestUpdateDate > currentUpdateDate
            };
        }

        private static AppInstallerResult DownloadItemsToTemp(AppInstallerArgument appInstallerArgument)
        {
            var tempPathForUpdateFiles = Path.Combine(appInstallerArgument.InstallDir, "..", "AppInstaller_Temp");
            appInstallerArgument.SourceDir.CopyDirectory(tempPathForUpdateFiles);

            var appInstallerForUpdatePath = Path.Combine(tempPathForUpdateFiles, appInstallerAssemblyName);

            appInstallerArgument.TempFolder = tempPathForUpdateFiles;
            appInstallerArgument.RunMode = RunMode.RunWithNewAppInTemp;

            appInstallerForUpdatePath.RunProcess(appInstallerArgument.ToCommandLineString());
            return new AppInstallerResult {ResultCode = ResultCode.Success};
        }

        private static AppInstallerResult RunWithNewAppInTemp(AppInstallerArgument appInstallerArgument)
        {
            WaitForExit(appInstallerArgument);

            appInstallerArgument.SourceDir.CopyDirectory(appInstallerArgument.InstallDir);

            var newInstallerPath = Path.Combine(appInstallerArgument.InstallDir, appInstallerAssemblyName);
            appInstallerArgument.RunMode = RunMode.CleanupAndRunApp;
            newInstallerPath.RunProcess(appInstallerArgument.ToCommandLineString());

            return new AppInstallerResult { ResultCode = ResultCode.Success, Updated = true };
        }

        private static void WaitForExit(AppInstallerArgument appInstallerArgument)
        {
            if (!string.IsNullOrEmpty(appInstallerArgument.OriginalAppPath))
            {
                var originalApp = Process.GetProcesses().FirstOrDefault(
                    p => p?.ProcessName == Path.GetFileName(appInstallerArgument.OriginalAppPath));
                originalApp?.WaitForExit();
            }

            // TODO: Exe終了判断（プロセス名からできないしどうしよう）。
            Thread.Sleep(1000);
        }

        private static AppInstallerResult CleanupAndRunApp(AppInstallerArgument argument)
        {
            // TODO: Exe終了判断（プロセス名からできないしどうしよう）。
            Thread.Sleep(1000);

            Directory.Delete(argument.TempFolder, true);
            argument.OriginalAppPath?.RunProcess();
            return new AppInstallerResult { ResultCode = ResultCode.Success, Updated = true };
        }
    }
}
