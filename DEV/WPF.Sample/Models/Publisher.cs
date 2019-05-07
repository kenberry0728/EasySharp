using System.Reflection;
using EasySharpStandard.Attributes.Core;
using EasySharpWpf.ViewModels.Rails.Attributes;

namespace EasySharpWpf.Sample.Models
{
    public class Publisher
    {
        [RailsBind]
        public string Name { get; set; }

        [RailsBind]
        public PublisherType PublisherType { get; set; }

        public override string ToString()
        {
            return this.ToCommaSeparatedString(
                p => p.GetCustomAttribute<RailsBindAttribute>()?.UserVisible == true);
        }
    }
}
