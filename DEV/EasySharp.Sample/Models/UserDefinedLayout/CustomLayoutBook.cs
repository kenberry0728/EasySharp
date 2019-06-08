using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EasySharpStandardMvvm.Attributes.Rails;
using EasySharpStandardMvvm.Models.Rails.Core;

namespace EasySharp.Sample.Models.UserDefinedLayout
{
    public class CustomLayoutBook : RailsModel, IValidatableObject
    {
        [RailsDataMemberBind("TitleTextBox")]
        [Required(ErrorMessage = "題名は必要です")]
        public string Title { get; set; }

        [RailsDataMemberBind("PriceTextBox")]
        [Range(0, 10000, ErrorMessage = "{0}～{1}円で入力してください。")]
        public int Price { get; set; }

        [RailsDataMemberBind("AuthorTextBox")]
        [Required(ErrorMessage = "著者名は必要です")]
        public string Author { get; set; }

        [RailsDataMemberBind("PublisherTextBox")]
        public string Publisher { get; set; }

        [RailsDataMemberBind("FreeTrialCheckBox")]
        public bool FreeTrial { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new ValidationResult[0];
        }
    }
}
