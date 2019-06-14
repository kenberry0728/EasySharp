using System;
using System.IO;
using System.Linq;
using AppInstaller.Core;
using AppInstaller.Core.Arguments;
using AppInstaller.Core.Results;
using EasySharpStandard.DiskIO;

namespace AppInstaller
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
            if (!args.Any())
            {
                return new Result { ResultCode = ResultCode.InvalidArgument };
            }

            var argument = new ArgumentConverter().ToInstance(args[0]);
            switch (argument.RunMode)
            {
                case RunMode.CheckUpdate:
                    return CheckUpdate(argument);
                case RunMode.Update:
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
