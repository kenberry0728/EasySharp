using System.Reflection;
using EasySharpStandardMvvm.ViewModels.Rails.Edit.Core;
using EasySharpXamarinForms.ViewModels.Rails.Edit.Core;
using Xamarin.Forms;

namespace EasySharpXamarinForms.ViewModels.Rails.Edit.Implementation
{
    internal class RailsBindCreator : RailsEditViewModelPathDefinition, IRailsBindCreator
    {
        public Binding CreateRailsBinding(PropertyInfo propertyInfo)
        {
            var bindingPath = this.GetRailsPropertyPath(propertyInfo);
            var binding = new Binding(bindingPath)
            {
                Mode = BindingMode.TwoWay,
            };

            return binding;
        }
    }
}
