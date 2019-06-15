using EasySharpStandard.DiskIO.Serializers;
using EasySharpStandard.Processes;

namespace AppInstaller.Core.Arguments
{
    public class ArgumentConverter : JsonStringConverter<Argument>
    {
        public string ToCommandLineString(Argument argument)
        {
            var json = base.ToString(argument);
            return json.ToCommandLineValue();
        }
    }
}