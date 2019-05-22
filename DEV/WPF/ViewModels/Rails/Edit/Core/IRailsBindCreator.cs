using System.Reflection;
using System.Windows.Data;

namespace EasySharpWpf.ViewModels.Rails.Edit.Core
{
    public interface IRailsBindCreator
    {
        Binding CreateRailsBinding(PropertyInfo propertyInfo);
        string GetPropertyName(PropertyInfo propertyInfo);
        string GetRailsPropertyName(string bindingPath);
        string GetRailsProperyPath(PropertyInfo propertyInfo);
    }
}