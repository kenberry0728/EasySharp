using EasySharpStandard.DiskIO.Serializers;
using EasySharpStandard.Processes;

namespace AppInstaller.Core.Arguments
{
    public class ArgumentConverter : JsonStringConverter<Argument>
    {
        public override string ToString(Argument argument)
        {
            var json = base.ToString(argument);
            return json.ToCommandLineValue();
        }
    }
}