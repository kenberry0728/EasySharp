using System.Linq;

using EasySharpStandard.Attributes.Core;
using EasySharpStandard.Reflections.Core;
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
            return string.Join(
                    ", ", 
                    this.GetType().GetProperties()
                    .Where(p => p.HasCustomAttribute<RailsBindAttribute>())
                    .Select(p => $"{p.GetDisplayName()} : {p.GetValue(this)}"));
        }
    }
}
