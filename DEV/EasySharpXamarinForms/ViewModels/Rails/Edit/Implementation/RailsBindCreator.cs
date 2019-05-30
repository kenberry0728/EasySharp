using EasySharpWpf.ViewModels.Rails.Edit.Core;
using EasySharpWpf.ViewModels.Rails.Edit.Implementation;
using System.Reflection;
using Xamarin.Forms;

namespace EasySharpXamarinForms.Sample.ViewModels.Rails.Edit.Implementation
{
    internal class RailsBindCreator : RailsBindCreatorBase<Binding>, IRailsBindCreator<Binding>
    {
        public override Binding CreateRailsBinding(PropertyInfo propertyInfo)
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
