using System.Windows;

namespace EasySharpWpf.Views.Rails.Core.Edit.Interfaces
{
    public interface IRailsEditViewFactory<T>
        where T : class, new()
    {
        FrameworkElement CreateEditView(T model);

        bool? ShowEditWindow(T model);
    }
}