using EasySharpStandardMvvm.ViewModels.Core;
using EasySharpWpf.ViewModels.Rails.Edit.Core;
using System.Reflection;

namespace EasySharpWpf.ViewModels.Rails.Edit.Implementation
{
    public class RailsBindPathCreator : ViewModelBase, IRailsBindPathCreator
    {
        public string GetRailsProperyPath(PropertyInfo propertyInfo)
        {
            return $"[{this.GetPropertyName(propertyInfo)}]";
        }

        public string GetRailsPropertyName(string bindingPath)
        {
            return bindingPath.Trim(new[] { '[', ']' });
        }

        public string GetPropertyName(PropertyInfo propertyInfo)
        {
            return propertyInfo.Name;
        }
    }
}
