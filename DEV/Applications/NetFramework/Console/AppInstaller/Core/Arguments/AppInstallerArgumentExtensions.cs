using AppInstaller.Implementation;

namespace AppInstaller.Core.Arguments
{
    public static class AppInstallerArgumentExtensions
    {
        public static string ToCommandLineString(this AppInstallerArgument arg, bool escape = true)
        {
            if (escape)
            {
                return new AppInstallerArgumentConverter().ToCommandLineString(arg);
            }
            else
            {
                return new AppInstallerArgumentConverter().ToString(arg);
            }
        }

        public static AppInstallerArgument AppInstallerArgumentFromString(this string text)
        {
            return new AppInstallerArgumentConverter().FromString(text);
        }
    }
}