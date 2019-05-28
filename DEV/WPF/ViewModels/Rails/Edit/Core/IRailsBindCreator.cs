using System.Reflection;
using System.Windows.Data;

namespace EasySharpWpf.ViewModels.Rails.Edit.Core
{
    public interface IRailsBindCreator : IRailsBindPathCreator
    {
        Binding CreateRailsBinding(PropertyInfo propertyInfo);
    }
}