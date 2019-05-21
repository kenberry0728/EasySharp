using EasySharpWpf.ViewModels.Rails.Edit.Core;
using EasySharpWpf.Views.ValidationRules.Core;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows.Data;

namespace EasySharpWpf.ViewModels.Rails.Edit.Implementation
{
    internal class RailsBindCreator : ViewModelBase, IRailsBindCreator
    {
        public string GetRailsProperyPath(PropertyInfo propertyInfo)
        {
            return $"[{propertyInfo.Name}]";
        }

        public string GetRailsPropertyName(string bindingPath)
        {
            return bindingPath.Trim(new[] { '[', ']' });
        }

        public Binding CreateRailsBinding(PropertyInfo propertyInfo)
        {
            var bindingPath = this.GetRailsProperyPath(propertyInfo);
            var binding = new Binding(bindingPath)
            {
                Mode = BindingMode.TwoWay,
                ValidatesOnNotifyDataErrors = true
            };

            return binding;
        }

        private static void AddValidationRule(Binding binding, ValidationAttribute validationAttribute)
        {
            // TODO: support input value validation.
            switch (validationAttribute)
            {
                case RequiredAttribute required:
                    binding.ValidationRules.Add(new RequiredValidationRule(required));
                    break;
                case RangeAttribute rangeAttribute:
                    binding.ValidationRules.Add(new RangeValidationRule(rangeAttribute));
                    break;
                // TODO StringLengthAttribute
                // TODO RegularExpressionAttribute
                // TODO CustomValidationAttribute
                default:
                    binding.ValidationRules.Add(new ValidationAttributeRuleBase(validationAttribute));
                    break;
            }
        }

    }
}
