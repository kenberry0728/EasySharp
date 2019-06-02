﻿using EasySharpStandardMvvm.ViewModels.Rails.Edit.Core;
using EasySharpWpf.ViewModels.Rails.Edit.Core;
using System;

namespace EasySharpWpf.Views.Rails.Core.Edit
{
    public interface IRailsEditViewFactory<TBinding, TUIBase>
    {
        IRailsBindCreator<TBinding> RailsBindCreator { get; }
        TUIBase CreateEditView(object model, Type type = null);
        bool? ShowEditWindow(object initialValueModel, Type type, out object editedModel);
        bool? ShowEditWindow(Type type, out object editedModel);
        void Edit(IRailsEditViewModel viewModel);
    }
}