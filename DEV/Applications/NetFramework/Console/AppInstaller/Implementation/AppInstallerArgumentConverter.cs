using AppInstaller.Core.Arguments;
using EasySharp.IO.Serializers;
using EasySharpStandard.Processes;

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