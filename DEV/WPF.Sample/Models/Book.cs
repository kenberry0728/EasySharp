using EasySharpWpf.ViewModels.Rails.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EasySharpWpf.Sample.Models
{
    [DisplayName("本")]
    public class Book : IValidatableObject
    {
        [DisplayName("題名")]
        [RailsBind]
        [Required(ErrorMessage = "題名は必要です")]
        public string Title { get; set; }

        [DisplayName("価格")]
        [RailsBind]
        [Range(0, 10000, ErrorMessage = "{0}～{1}円で入力してください。")]
        public int Price { get; set; }

        [DisplayName("著者")]
        [RailsBind]
        [Required(ErrorMessage = "著者名は必要です")]
        public string Author { get; set; }

        [DisplayName("出版社")]
        [RailsBind]
        public Publisher Publisher { get; set; }

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
