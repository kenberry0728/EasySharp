using System.Reflection;
using EasySharpStandardMvvm.Views.Layouts.ViewModels.Core;

namespace EasySharpStandardMvvm.ViewModels.Rails.Edit.Core
{
    public abstract class RailsEditViewModelPathDefinition : ViewModelBase, IRailsEditViewModelPathDefinition
    {
        public string GetRailsPropertyPath(PropertyInfo propertyInfo)
        {
            return $"[{this.GetPropertyName(propertyInfo)}]";
        }

        public string GetRailsPropertyName(string bindingPath)
        {
            return bindingPath.Trim('[', ']');
        }

        public string GetPropertyName(PropertyInfo propertyInfo)
        {
            return propertyInfo.Name;
        }
    }
}
