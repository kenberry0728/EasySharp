using System.Reflection;

namespace EasySharpStandardMvvm.ViewModels.Rails.Edit.Core
{
    public interface IRailsEditViewModelPathDefinition
    {
        string GetRailsPropertyPath(PropertyInfo propertyInfo);
        string GetRailsPropertyName(string bindingPath);
        string GetPropertyName(PropertyInfo propertyInfo);
    }
}