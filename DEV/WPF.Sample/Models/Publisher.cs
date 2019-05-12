using EasySharpWpf.Models.Core;
using EasySharpWpf.ViewModels.Rails.Attributes;
using System.ComponentModel.DataAnnotations;

namespace EasySharpWpf.Sample.Models
{
    public class Publisher : RailsModel
    {
        [RailsBind]
        [Required]
        public string Name { get; set; }

        [RailsBind]
        public PublisherType PublisherType { get; set; }
    }
}
