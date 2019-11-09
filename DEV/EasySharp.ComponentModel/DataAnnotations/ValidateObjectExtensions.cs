using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace EasySharp.ComponentModel.DataAnnotations
{
    public static class ValidateObjectExtensions
    {
        public static IEnumerable<ValidationResult> Validate(this object model)
        {
            var validationResults = new List<ValidationResult>();

            // Note: second argument can be used to inject external service.
            var validationContext = new ValidationContext(model, null, null);

            Validator.TryValidateObject(
                model,
                validationContext,
                validationResults,
                true);

            return validationResults;
        }

        public static IEnumerable<ValidationResult> ValidateProperty(
            this object model, string propertyName, object value)
        {
            var context = new ValidationContext(model) { MemberName = propertyName };
            var validationErrors = new List<ValidationResult>();
            Validator.TryValidateProperty(value, context, validationErrors);
            return validationErrors;
        }

        public static IEnumerable<ValidationResult> ValidateProperty(
            this object model, PropertyInfo propertyInfo)
        {
            return model.ValidateProperty(propertyInfo.Name, propertyInfo.GetValue(model));
        }
    }
}
