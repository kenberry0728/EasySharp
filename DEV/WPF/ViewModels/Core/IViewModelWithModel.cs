using System.Reflection;

namespace EasySharpWpf.ViewModels.Core
{
    public interface IViewModelWithModel<T>
    {
        T Model { get; }

        string GetBindingPath(PropertyInfo propertyInfo);
    }
}
