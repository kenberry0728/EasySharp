using EasySharpStandardMvvm.Models.Rails.Core;
using EasySharpStandardMvvm.Rails.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EasySharpWpf.Sample.Models.AutoLayout
{
    public class Publisher : RailsModel
    {
        [RailsBind]
        [DisplayName("出版社名")]
        [Required(ErrorMessage ="出版社名は必要です")]
        public string Name { get; set; }

        [DisplayName("個人/企業")]
        [RailsBind]
        public PublisherType PublisherType { get; set; }
    }
}
