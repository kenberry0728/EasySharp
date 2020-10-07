using EasySharp.ComponentModel.DataAnnotations;
using EasySharp.IO.Reflection;
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
    [DisplayName("本")]
    public class Book : RailsModel, IValidatableObject
    {
        [DisplayName("題名")]
        [RailsDataMemberBind]
        [Required(ErrorMessage = "題名は必要です")]
        [StringLength(20, ErrorMessage = "{0}は{1}文字までにしてください")]
        public string Title { get; set; } = string.Empty;

        [DisplayName("価格")]
        [RailsDataMemberBind]
        [Range(0, 10000, ErrorMessage = "{0}は{1}～{2}円で入力してください。")]
        public int Price { get; set; } = 0;

        [DisplayName("著者")]
        [RailsDataMemberCandidatesStringBind(nameof(Authors))]
        [Required(ErrorMessage = "著者名は必要です")]
        public string Author { get; set; } = string.Empty;

        [RailsCandidatesStringSourceBind(
            DislayMemberPath = nameof(StringUIValue.DisplayValue),
            SelectedValuePath = nameof(StringUIValue.Value))]
        [IgnoreDataMember]
        public IEnumerable<StringUIValue> Authors => lazyAuthors.Value;

        // TODO:if rmoving lazy stack overflow occurs when select value
        private static readonly Lazy<IEnumerable<StringUIValue>> lazyAuthors
            = new Lazy<IEnumerable<StringUIValue>>(() =>
                typeof(Book).GetLocalResourceMultiValues(nameof(Authors), ".txt")
                            .Select(values => new StringUIValue(values[0], values[1])));

        [DisplayName("出版社")]
        [RailsDataMemberBind]
        [UserTypeMemberValidation]
        public Publisher Publisher { get; set; } = new Publisher();

        [DisplayName("無料サンプル")]
        [RailsDataMemberBind]
        public bool FreeTrial { get; set; }

        [DisplayName("発行日")]
        [RailsDataMemberBind]
        public DateTime PublishedDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // TODO
            // RailsCandidatesStringのValidation
            return Enumerable.Empty<ValidationResult>();
        }
    }
}
