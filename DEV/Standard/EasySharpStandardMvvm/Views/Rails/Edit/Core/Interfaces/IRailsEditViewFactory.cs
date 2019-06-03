using EasySharpStandardMvvm.ViewModels.Rails.Edit.Core;
using EasySharpWpf.ViewModels.Rails.Edit.Core;
using System;

namespace EasySharpWpf.Views.Rails.Core.Edit
{
    public interface IRailsEditViewFactory<TBinding, TViewControl>
    {
        IRailsBindCreator<TBinding> RailsBindCreator { get; }
        TViewControl CreateEditView(object model, Type type = null);
        bool? ShowEditView(object initialValueModel, Type type, out object editedModel);
        bool? ShowEditWindow(Type type, out object editedModel);
        void Edit(IRailsEditViewModel viewModel);
    }
}