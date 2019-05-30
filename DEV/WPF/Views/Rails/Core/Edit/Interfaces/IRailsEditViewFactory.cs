using EasySharpStandardMvvm.ViewModels.Core;
using EasySharpWpf.ViewModels.Rails.Edit.Core;
using System;
using System.Windows;
using System.Windows.Data;

namespace EasySharpWpf.Views.Rails.Core.Edit
{
    public interface IRailsEditViewFactory
    {
        IRailsBindCreator<Binding> RailsBindCreator { get; }
        FrameworkElement CreateEditView(object model, Type type = null);
        bool? ShowEditWindow(object initialValueModel, Type type, out object editedModel);
        bool? ShowEditWindow(Type type, out object editedModel);
        void Edit(IRailsEditViewModel viewModel);
    }
}