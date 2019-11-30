using EasySharp;
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Checked")]
        public string GetRailsPropertyName(string bindingPath)
        {
            bindingPath.ThrowArgumentNullOrEmptyException(nameof(bindingPath));
            return bindingPath.Trim('[', ']');
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
        public string GetPropertyName(PropertyInfo propertyInfo)
        {
            propertyInfo.ThrowArgumentExceptionIfNull(nameof(propertyInfo));
            return propertyInfo.Name;
        }
    }
}
