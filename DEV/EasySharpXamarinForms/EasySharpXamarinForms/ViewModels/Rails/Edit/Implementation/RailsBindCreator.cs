using EasySharpWpf.ViewModels.Rails.Edit.Implementation;
using EasySharpXamarinForms.ViewModels.Rails.Edit.Core;
using System.Reflection;
using Xamarin.Forms;

namespace EasySharpXamarinForms.ViewModels.Rails.Edit.Implementation
{
    internal class RailsBindCreator : RailsBindPathCreator, IRailsBindCreator
    {
        public Binding CreateRailsBinding(PropertyInfo propertyInfo)
        {
            var bindingPath = this.GetRailsProperyPath(propertyInfo);
            var binding = new Binding(bindingPath)
            {
                Mode = BindingMode.TwoWay,
            };

            return binding;
        }
    }
}
