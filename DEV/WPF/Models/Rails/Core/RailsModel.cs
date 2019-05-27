using EasySharpStandard.Attributes.Core;
using EasySharpStandard.Rails.Attributes;
using System.Reflection;

namespace EasySharpWpf.Models.Rails.Core
{
    public class RailsModel
    {
        public override string ToString()
        {
            return this.ToCommaSeparatedString(
                p => p.GetCustomAttribute<RailsBindAttribute>()?.UserVisible == true);
        }
    }
}
