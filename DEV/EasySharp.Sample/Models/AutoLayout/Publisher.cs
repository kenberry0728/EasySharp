using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EasySharpStandard.Reflections.Core.LocalResources;
using EasySharpStandardMvvm.Attributes.Rails;
using EasySharpStandardMvvm.Models.Rails.Core;

namespace EasySharp.Sample.Models.AutoLayout
{
    public class Publisher : RailsModel
    {
        [DisplayName("出版社名")]
        [Required(ErrorMessage ="出版社名は必要です")]
        [RailsDataMemberCandidatesStringBind(nameof(Names))]
        public string Name { get; set; }

        [RailsCandidatesStringSourceBind(DependentPropertyName = nameof(PublisherType))]
        public IDictionary<string, List<string>> Names => lazySelectableNames.Value;

        private static readonly Lazy<IDictionary<string, List<string>>> lazySelectableNames
            = new Lazy<IDictionary<string, List<string>>>(() =>
                typeof(Publisher).GetLocalResourceDependentValues(nameof(Names)));

        [DisplayName("個人/企業")]
        [RailsDataMemberBind]
        public PublisherType PublisherType { get; set; }

    }
}
