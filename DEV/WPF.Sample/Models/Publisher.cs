using EasySharpWpf.Models.Core;
using EasySharpWpf.ViewModels.Rails.Attributes;

namespace EasySharpWpf.Sample.Models
{
    public class Publisher : RailsModel
    {
        [RailsBind]
        public string Name { get; set; }

        [RailsBind]
        public PublisherType PublisherType { get; set; }
    }
}
