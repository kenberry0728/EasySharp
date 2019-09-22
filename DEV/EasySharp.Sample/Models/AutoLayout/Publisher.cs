using EasySharpStandard.Reflections.Core.LocalResources;
using EasySharpStandardMvvm.Attributes.Rails;
using EasySharpStandardMvvm.Models.Rails.Core;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace EasySharp.Sample.Models.AutoLayout
{
    public class Publisher : RailsModel
    {
        [DisplayName("個人/企業")]
        [RailsDataMemberBind]
        public PublisherType PublisherType { get; set; }

        [DisplayName("出版社名")]
        [Required(ErrorMessage = "出版社名は必要です")]
        [RailsDataMemberCandidatesStringBind(nameof(Names))]
        public string Name { get; set; }

        [RailsCandidatesStringSourceBind(DependentPropertyName = nameof(PublisherType))]
        [IgnoreDataMember]
        public IDictionary<string, List<string>> Names => typeof(Publisher).GetLocalResourceDependentValues(nameof(Names), ".txt");
    }
}
