
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace EasySharpWpf.Views.ValidationRules.Core
{
    public class RequiredValidationRule : ValidationAttributeRuleBase
    {
        public RequiredValidationRule(
            RequiredAttribute requiredAttribute)
            : base(requiredAttribute)
        {
        }

        protected override string DefaultErrorMessage => "値が必要です";
    }
}
