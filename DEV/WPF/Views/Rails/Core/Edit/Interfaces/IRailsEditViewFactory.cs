using EasySharpWpf.ViewModels.Rails.Core.Edit;
using EasySharpWpf.ViewModels.Rails.Edit.Core;
using System;
using System.Windows;

namespace EasySharpWpf.Views.Rails.Core.Edit
{
    public interface IRailsEditViewFactory
    {
        IRailsBindCreator RailsBindCreator { get; }
        FrameworkElement CreateEditView(object model, Type type = null);
        bool? ShowEditWindow(object initialValueModel, Type type, out object editedModel);
        bool? ShowEditWindow(Type type, out object editedModel);
        void Edit(IRailsEditViewModel viewModel);
    }
}