using AppInstaller.Implementation;

namespace AppInstaller.Core.Results
{
    public static class AppInstallerResultExtensions
    {
        public static AppInstallerResult AppInstallerResultFromString(this string text)
        {
            return new AppInstallerResultConverter().FromString(text);
        }

        public static string ToString(this AppInstallerResult result)
        {
            return new AppInstallerResultConverter().ToString(result);
        }
    }
}