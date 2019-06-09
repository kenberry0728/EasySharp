using System.Reflection;

namespace EasySharpStandardMvvm.Views.Layouts.ViewModels.Rails.Core
{
    public interface IRailsBindCreator<TBinding>
    {
        string GetPropertyName(PropertyInfo propertyInfo);
        string GetRailsPropertyName(string bindingPath);
        string GetRailsPropertyPath(PropertyInfo propertyInfo);
        TBinding CreateRailsBinding(PropertyInfo propertyInfo);
    }
}