using System.Reflection;
using EasySharpStandardMvvm.Views.Layouts.ViewModels.Core;

namespace EasySharpStandardMvvm.Views.Layouts.ViewModels.Rails.Core
{
    public abstract class RailsBindCreatorBase<TBinding> : ViewModelBase, IRailsBindCreator<TBinding>
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

        public abstract TBinding CreateRailsBinding(PropertyInfo propertyInfo);
    }
}
