using EasySharpStandard.Reflections.Core.LocalResources;
using EasySharpStandardMvvm.Attributes.Rails;
using EasySharpStandardMvvm.Models.Rails.Core;
using EasySharpStandardMvvm.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        [RailsCandidatesStringSourceBind(
            DependentPropertyName = nameof(PublisherType),
            DislayMemberPath = nameof(StringUIValue.DisplayValue),
            SelectedValuePath = nameof(StringUIValue.Value))]
        [IgnoreDataMember]
        public IDictionary<string, List<StringUIValue>> Names
            => lazyNames.Value;

        // TODO:if rmoving lazy, stack overflow occurs when select value
        private static readonly Lazy<IDictionary<string, List<StringUIValue>>> lazyNames
            = new Lazy<IDictionary<string, List<StringUIValue>>>(() =>
            typeof(Publisher).GetLocalResourceDependentMultiValues(nameof(Names), ".txt")
            .ToDictionary(
                d => d.Key,
                d => d.Value.Select(values => new StringUIValue(values[0], values[1])).ToList()));

    }
}
