using System.Reflection;

namespace EasySharpWpf.ViewModels.Rails.Edit.Core
{
    public interface IRailsBindCreator
    {
        System.Windows.Data.Binding CreateRailsBinding(PropertyInfo propertyInfo);
        string GetRailsPropertyName(string bindingPath);
        string GetRailsProperyPath(PropertyInfo propertyInfo);
    }
}