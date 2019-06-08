using EasySharpWpf.ViewModels.Rails.Edit.Implementation;
using EasySharpXamarinForms.Views.Rails.Core.Edit.Interfaces;
using System.Reflection;
using Xamarin.Forms;

namespace EasySharpXamarinForms.Sample.ViewModels.Rails.Edit.Implementation
{
    internal class RailsBindCreator : RailsBindCreatorBase<Binding>, IRailsBindCreator
    {
        public override Binding CreateRailsBinding(PropertyInfo propertyInfo)
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
