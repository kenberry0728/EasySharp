using EasySharpWpf.Models.Core;
using EasySharpWpf.ViewModels.Rails.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasySharpWpf.Sample.Models.UserDefinedLayout
{
    public class CustomLayoutBook : RailsModel, IValidatableObject
    {
        [RailsBind(ElementName = "TitleTextBox")]
        [Required(ErrorMessage = "題名は必要です")]
        public string Title { get; set; }

        [RailsBind(ElementName = "PriceTextBox")]
        [Range(0, 10000, ErrorMessage = "{0}～{1}円で入力してください。")]
        public int Price { get; set; }

        [RailsBind(ElementName = "AuthorTextBox")]
        [Required(ErrorMessage = "著者名は必要です")]
        public string Author { get; set; }

        [RailsBind(ElementName = "PublisherTextBox")]
        public string Publisher { get; set; }

        [RailsBind(ElementName = "FreeTrialCheckBox")]
        public bool FreeTrial { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new ValidationResult[0];
        }
    }
}
