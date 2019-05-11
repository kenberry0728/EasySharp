﻿using EasySharpWpf.ViewModels.Core;
using EasySharpWpf.Views.ValidationRules.Core;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows.Data;

namespace EasySharpWpf.Views.Rails.Implementations
{
    internal static class RailsBindCreator
    {
        public static string GetRailsBindingPath(this PropertyInfo propertyInfo)
        {
            return $"[{propertyInfo.Name}]";
        }

        public static Binding CreateRailsBinding(this PropertyInfo propertyInfo)
        {
            var bindingPath = propertyInfo.GetRailsBindingPath();
            var binding = new Binding(bindingPath)
            {
                Mode = BindingMode.TwoWay,
            };

            var validationAttributes = propertyInfo.GetCustomAttributes<ValidationAttribute>();
            foreach (var validationAttribute in validationAttributes)
            {
                AddValidationRule(binding, validationAttribute);
            }

            return binding;
        }

        private static void AddValidationRule(Binding binding, ValidationAttribute validationAttribute)
        {
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
