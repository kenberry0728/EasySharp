using System;
using System.Windows;

namespace EasySharpWpf.Views.Rails.Core.Edit
{
    public interface IRailsEditViewFactory
    {
        FrameworkElement CreateEditView(object model, Type type = null);
        bool? ShowEditWindow(object initialValueModel, Type type, out object editedModel);
        bool? ShowEditWindow(Type type, out object editedModel);
    }
}