using System;
using System.Windows;

namespace EasySharpWpf.Views.Rails.Core.Edit.Interfaces
{
    public interface IRailsEditViewBinder
    {
        void ApplyRailsBinding(FrameworkElement rootElement, object model);

        void ApplyRailsBinding(FrameworkElement rootElement, object model, Type type);
    }
}