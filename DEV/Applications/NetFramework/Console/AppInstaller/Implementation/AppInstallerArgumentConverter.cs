using AppInstaller.Core.Arguments;
using EasySharp.Diagnostics;
using EasySharp.Runtime.Serialization.Json;

namespace AppInstaller.Implementation
{
    internal class AppInstallerArgumentConverter : JsonStringConverter<AppInstallerArgument>
    {
        public string ToCommandLineString(AppInstallerArgument appInstallerArgument)
        {
            var json = base.ToString(appInstallerArgument);
            return json.ToCommandLineValue();
        }
    }
}