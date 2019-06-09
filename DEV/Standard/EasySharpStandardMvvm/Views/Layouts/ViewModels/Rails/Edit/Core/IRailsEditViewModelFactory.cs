using System;
using EasySharpStandardMvvm.Views.Layouts.ViewModels.Rails.Core;

namespace EasySharpStandardMvvm.Views.Layouts.ViewModels.Rails.Edit.Core
{
    public interface IRailsEditViewModelFactory<TBinding>
    {
        IRailsBindCreator<TBinding> RailsBindCreator { get; }

        IRailsEditViewModel Create(object model, Type type = null);
    }
}
