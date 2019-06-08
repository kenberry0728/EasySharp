using EasySharpStandardMvvm.ViewModels.Core;
using EasySharpWpf.ViewModels.Rails.Edit.Core;
using System.Reflection;

namespace EasySharpWpf.ViewModels.Rails.Edit.Implementation
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
