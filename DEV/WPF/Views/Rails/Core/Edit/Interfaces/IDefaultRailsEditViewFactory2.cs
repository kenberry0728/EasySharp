using System;
using System.Windows;

namespace EasySharpWpf.Views.Rails.Core.Edit
{
    public interface IRailsEditViewFactory2
    {
        FrameworkElement CreateEditView(object model, Type type = null);
        bool? ShowEditWindow(object model, Type type, out object editedModel);
    }
}