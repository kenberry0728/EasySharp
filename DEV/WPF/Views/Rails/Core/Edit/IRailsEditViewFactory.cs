using System.Windows;

namespace EasySharpWpf.Views.Rails.Core.Edit
{
    public interface IRailsEditViewFactory<T>
    {
        FrameworkElement CreateEditView(T model);
    }
}