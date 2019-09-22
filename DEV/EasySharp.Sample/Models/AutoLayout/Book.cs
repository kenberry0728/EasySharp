using EasySharpStandard.Reflections.Core.LocalResources;
using EasySharpStandard.Validations.Core.Attributes;
using EasySharpStandardMvvm.Attributes.Rails;
using EasySharpStandardMvvm.Models.Rails.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        [RailsDataMemberCandidatesStringBind(nameof(SelectableAuthors))]
        [Required(ErrorMessage = "著者名は必要です")]
        public string Author { get; set; } = string.Empty;

        #region SelectableAuthors

        [RailsCandidatesStringSourceBind]
        [IgnoreDataMember]
        public IEnumerable<string> SelectableAuthors 
            => typeof(Book).GetLocalResourceValues(nameof(SelectableAuthors), ".txt");

        #endregion

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
            return new ValidationResult[0];
        }
    }
}
