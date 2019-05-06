using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Windows.Controls;

namespace EasySharpWpf.Views.ValidationRules.Core
{
    public class ValidationAttributeRuleBase : ValidationRule
    {
        private readonly ValidationAttribute validationAttribute;

        internal ValidationAttributeRuleBase(ValidationAttribute validationAttribute)
        {
            this.validationAttribute = validationAttribute;
        }

        public string ErrorMessage
        {
            get
            {
                return string.IsNullOrEmpty(this.validationAttribute.ErrorMessage) 
                    ? this.DefaultErrorMessage 
                    : this.validationAttribute.ErrorMessage;
            }
        }

        protected virtual string DefaultErrorMessage => "値が不正です";

        public override System.Windows.Controls.ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return this.validationAttribute.IsValid(value)
                ? System.Windows.Controls.ValidationResult.ValidResult
                : new System.Windows.Controls.ValidationResult(false, this.ErrorMessage);
        }
    }
}
