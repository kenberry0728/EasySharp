using System;
using EasySharpStandardMvvm.Views.Layouts.ViewModels.Rails.Core;
using EasySharpStandardMvvm.Views.Layouts.ViewModels.Rails.Edit.Core;

namespace EasySharpStandardMvvm.Views.Rails.Edit.Core.Interfaces
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