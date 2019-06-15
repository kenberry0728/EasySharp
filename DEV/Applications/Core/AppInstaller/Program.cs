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
            var arg = new Argument() {RunMode = RunMode.Update, SourceDir = @"c:\testDir"};
            var commandLineArg = new ArgumentConverter().ToCommandLineString(arg);
            #endregion

            Result modeResult;
            try
            {
                modeResult = InternalMain(args);
            }
            catch (Exception e)
            {
                modeResult = new Result
                {
                    ResultCode = ResultCode.InternalError,
                    Message = e.Message
                };
            }

            Console.WriteLine(new UpdateResultConverter().ToString(modeResult));
        }

        private static Result InternalMain(string[] args)
        {
            const string argBackupFilePath = "AppInstaller_UpdateArg.json";
            var arg = args.Any() ? args[0] : argBackupFilePath.ReadToEnd();

            var argument = new ArgumentConverter().ToInstance(arg);
            switch (argument.RunMode)
            {
                case RunMode.CheckUpdate:
                    return CheckUpdate(argument);
                case RunMode.Update:
                    argBackupFilePath.WriteToFile(arg);
                    return UpdateDirectory(argument);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static Result CheckUpdate(Argument argument)
        {
            var sourceDirInfo = new DirectoryInfo(argument.SourceDir);
            var latestUpdateDate = sourceDirInfo.GetFiles("*", SearchOption.AllDirectories)
                .Max(f => f.LastWriteTimeUtc);

            var installDirInfo = new DirectoryInfo(argument.InstallDir);
            var currentUpdateDate = installDirInfo.GetFiles("*", SearchOption.AllDirectories)
                .Max(f => f.LastWriteTimeUtc);
            return new Result
            {
                ResultCode = ResultCode.Success,
                Updated = latestUpdateDate > currentUpdateDate
            };
        }

        private static Result UpdateDirectory(Argument argument)
        {
            // TODO: Delete obsolete files. It needs ignore file logic.
            var tempPath = Path.Combine(argument.InstallDir, "..", Guid.NewGuid().ToString());
            argument.SourceDir.CopyDirectory(tempPath);
            tempPath.CopyDirectory(argument.InstallDir);

            Directory.Delete(tempPath, true);
            return new Result { ResultCode = ResultCode.Success};
        }
    }
}
