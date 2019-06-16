using System.Reflection;

namespace AppInstaller.Core
{
    public static class AppInstallerConstants
    {
        public static readonly string AppFilePath = Assembly.GetAssembly(typeof(AppInstallerConstants)).GetName().Name;

    }
}