using EasySharpStandard.Validations.Core.Attributes;
using EasySharpStandardMvvm.Rails.Attributes;
using EasySharpWpf.Models.Rails.Core;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EasySharpWpf.Sample.Models.AutoLayout
{
    [DisplayName("本")]
    public class Book : RailsModel, IValidatableObject
    {
        [DisplayName("題名")]
        [RailsBind]
        [Required(ErrorMessage = "題名は必要です")]
        [StringLength(20, ErrorMessage = "{0}は{1}文字までにしてください")]
        public string Title { get; set; } = string.Empty;

        [DisplayName("価格")]
        [RailsBind]
        [Range(0, 10000, ErrorMessage = "{0}は{1}～{2}円で入力してください。")]
        public int Price { get; set; } = 0;

        [DisplayName("著者")]
        [RailsBind]
        [Required(ErrorMessage = "著者名は必要です")]
        public string Author { get; set; } = string.Empty;

        [DisplayName("出版社")]
        [RailsBind]
        [UserTypeMemberValidation]
        public Publisher Publisher { get; set; } = new Publisher();

        [DisplayName("無料サンプル")]
        [RailsBind]
        public bool FreeTrial { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // TODO
            return new ValidationResult[0];
        }
    }
}
