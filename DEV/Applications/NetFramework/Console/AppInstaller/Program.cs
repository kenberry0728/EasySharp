using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using AppInstaller.Core.Arguments;
using AppInstaller.Core.Results;
using AppInstaller.Implementation;
using AppInstaller.RunModes;
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
                return;
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

                e.Message.WriteToFile("Error.log");
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

            switch (argument.RunMode)
            {
                case RunMode.CheckUpdate:
                    return new CheckUpdate().Run(
                        argument.SourceDir,
                        argument.InstallDir,
                        argument.ExcludePathRegex);
                case RunMode.DownloadAppInstallerToTemp:
                    return new DownloadAppInstallerToTemp(appInstallerAssemblyName).Run(argument);
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
