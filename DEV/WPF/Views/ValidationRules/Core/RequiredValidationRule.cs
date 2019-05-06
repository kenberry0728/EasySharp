using System.Globalization;
using System.Windows.Controls;

using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace EasySharpWpf.Views.ValidationRules.Core
{
    public class RequiredValidationRule : ValidationAttributeRuleBase
    {
        private readonly RequiredAttribute requiredAttribute;

        public RequiredValidationRule(
            RequiredAttribute requiredAttribute)
            : base(requiredAttribute)
        {
            this.requiredAttribute = requiredAttribute;
        }

        protected override string DefaultErrorMessage => "値が必要です";

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return this.requiredAttribute.IsValid(value) 
                ? ValidationResult.ValidResult 
                : new ValidationResult(false, this.ErrorMessage);
        }
    }
}
