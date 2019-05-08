using System.Windows;

namespace EasySharpWpf.Views.Rails.Core.Edit.Interfaces
{
    public interface IRailsEditViewBinder<TModel>
    {
        void ApplyRailsBinding(FrameworkElement frameworkElement, TModel model);
    }
}