using System.Reflection;
using Xamarin.Forms;

namespace EasySharpXamarinForms.ViewModels.Rails.Edit.Core
{
    public interface IRailsBindCreator
    {
        Binding CreateRailsBinding(PropertyInfo propertyInfo);
        string GetPropertyName(PropertyInfo propertyInfo);
        string GetRailsPropertyName(string bindingPath);
        string GetRailsProperyPath(PropertyInfo propertyInfo);
    }
}