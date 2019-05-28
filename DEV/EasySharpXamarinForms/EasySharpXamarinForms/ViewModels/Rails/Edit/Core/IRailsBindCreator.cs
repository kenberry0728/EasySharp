using EasySharpWpf.ViewModels.Rails.Edit.Core;
using System.Reflection;
using Xamarin.Forms;

namespace EasySharpXamarinForms.ViewModels.Rails.Edit.Core
{
    public interface IRailsBindCreator : IRailsBindPathCreator
    {
        Binding CreateRailsBinding(PropertyInfo propertyInfo);
    }
}