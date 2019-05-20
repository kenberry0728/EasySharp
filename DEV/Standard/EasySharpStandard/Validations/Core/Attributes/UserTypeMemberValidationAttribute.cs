using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EasySharpStandard.Validations.Core.Attributes
{
    public class UserTypeMemberValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // TODO: Consider Context
            // 
            var results = value.Validate();
            if (results.Any())
            {
                return new ValidationResult(FormatErrorMessage(results));
            }

            return null;
        }

        private string FormatErrorMessage(IEnumerable<ValidationResult> results)
        {
            return string.Join(Environment.NewLine, results.Select(r => ErrorMessage));
        }
    }
}
