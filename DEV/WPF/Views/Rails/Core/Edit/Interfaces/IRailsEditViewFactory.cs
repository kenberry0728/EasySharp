using System.Windows;

namespace EasySharpWpf.Views.Rails.Core.Edit.Interfaces
{
    public interface IRailsEditViewFactory<T>
    {
        FrameworkElement CreateEditView(T model);
    }
}