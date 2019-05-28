using System.Reflection;

namespace EasySharpWpf.ViewModels.Rails.Edit.Core
{
    public interface IRailsBindPathCreator
    {
        string GetPropertyName(PropertyInfo propertyInfo);
        string GetRailsPropertyName(string bindingPath);
        string GetRailsProperyPath(PropertyInfo propertyInfo);
    }
}