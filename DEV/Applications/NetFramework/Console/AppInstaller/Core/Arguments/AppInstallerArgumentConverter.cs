using EasySharpStandard.DiskIO.Serializers;
using EasySharpStandard.Processes;

namespace AppInstaller.Core.Arguments
{
    public class AppInstallerArgumentConverter : JsonStringConverter<AppInstallerArgument>
    {
        public string ToCommandLineString(AppInstallerArgument appInstallerArgument)
        {
            var json = base.ToString(appInstallerArgument);
            return json.ToCommandLineValue();
        }
    }
}