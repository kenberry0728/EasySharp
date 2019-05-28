using EasySharpStandardMvvm.ViewModels.Core;
using EasySharpXamarinForms.ViewModels.Rails.Edit.Core;
using System.Reflection;
using Xamarin.Forms;

namespace EasySharpXamarinForms.ViewModels.Rails.Edit.Implementation
{
    internal class RailsBindCreator : ViewModelBase, IRailsBindCreator
    {
        public string GetRailsProperyPath(PropertyInfo propertyInfo)
        {
            return $"[{this.GetPropertyName(propertyInfo)}]";
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
            };

            return binding;
        }

        public string GetPropertyName(PropertyInfo propertyInfo)
        {
            return propertyInfo.Name;
        }
    }
}
