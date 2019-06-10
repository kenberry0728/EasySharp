using System.Reflection;
using EasySharpStandardMvvm.ViewModels.Rails.Edit.Core;

namespace EasySharpStandardMvvm.Views.Layouts.ViewModels.Rails.Core
{
    public interface IRailsBindCreator<TBinding> : IRailsEditViewModelPathDefinition
    {
        TBinding CreateRailsBinding(PropertyInfo propertyInfo);
    }
}