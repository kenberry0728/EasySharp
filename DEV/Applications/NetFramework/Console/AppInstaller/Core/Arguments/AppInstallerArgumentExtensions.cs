using AppInstaller.Implementation;

namespace AppInstaller.Core.Arguments
{
    public static class AppInstallerArgumentExtensions
    {
        public static string ToCommandLineString(this AppInstallerArgument arg)
        {
            return new AppInstallerArgumentConverter().ToCommandLineString(arg);
        }

        public static AppInstallerArgument AppInstallerArgumentFromString(this string text)
        {
            return new AppInstallerArgumentConverter().FromString(text);
        }
    }
}