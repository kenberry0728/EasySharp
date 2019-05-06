using System.Globalization;
using System.Windows.Controls;

using RangeAttribute = System.ComponentModel.DataAnnotations.RangeAttribute;

namespace EasySharpWpf.Views.ValidationRules.Core
{
    public class RangeValidationRule : ValidationAttributeRuleBase
    {
        private readonly RangeAttribute rangeAttribute;

        public RangeValidationRule(
            RangeAttribute validationAttribute)
            : base(validationAttribute)
        {
            this.rangeAttribute = validationAttribute;
        }

        protected override string DefaultErrorMessage => "{0}～{1}で入力してください。";

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (this.rangeAttribute.IsValid(value))
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                string errorMessage = string.Format(
                    CultureInfo.InvariantCulture,
                    this.ErrorMessage,
                    this.rangeAttribute.Minimum,
                    this.rangeAttribute.Maximum);
                return new ValidationResult(false, errorMessage);
            }
        }
    }
}
