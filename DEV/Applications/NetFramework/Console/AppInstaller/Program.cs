using System;
using System.IO;
using System.Linq;
using AppInstaller.Core;
using AppInstaller.Core.Arguments;
using AppInstaller.Core.Results;
using EasySharpStandard.DiskIO;
using EasySharpStandard.DiskIO.Serializers;

namespace AppInstaller
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region debug
            var arg = new AppInstallerArgument() {RunMode = RunMode.Update, SourceDir = @"c:\testDir"};
            var commandLineArg = new AppInstallerArgumentConverter().ToCommandLineString(arg);
            #endregion

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
            }

            Console.WriteLine(new AppInstallerResultConverter().ToString(modeAppInstallerResult));
        }

        private static AppInstallerResult InternalMain(string[] args)
        {
            const string argBackupFilePath = "AppInstaller_UpdateArg.json";
            var arg = args.Any() ? args[0] : argBackupFilePath.ReadToEnd();

            var argument = new AppInstallerArgumentConverter().ToInstance(arg);
            switch (argument.RunMode)
            {
                case RunMode.CheckUpdate:
                    return CheckUpdate(argument);
                case RunMode.Update:
                    arg.WriteToFile(argBackupFilePath);
                    return UpdateDirectory(argument);
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

        private static AppInstallerResult UpdateDirectory(AppInstallerArgument appInstallerArgument)
        {
            // TODO: Delete obsolete files. It needs ignore file logic.
            var tempPath = Path.Combine(appInstallerArgument.InstallDir, "..", Guid.NewGuid().ToString());
            appInstallerArgument.SourceDir.CopyDirectory(tempPath);
            tempPath.CopyDirectory(appInstallerArgument.InstallDir);

            Directory.Delete(tempPath, true);
            return new AppInstallerResult { ResultCode = ResultCode.Success, Updated = true };
        }
    }
}
