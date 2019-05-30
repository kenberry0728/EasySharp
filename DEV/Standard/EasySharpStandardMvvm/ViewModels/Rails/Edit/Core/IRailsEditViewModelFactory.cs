using EasySharpWpf.ViewModels.Rails.Edit.Core;
using System;

namespace EasySharpStandardMvvm.ViewModels.Rails.Edit.Core
{
    public interface IRailsEditViewModelFactory<TBinding>
    {
        IRailsBindCreator<TBinding> RailsBindCreator { get; }

        IRailsEditViewModel Create(object model);

        IRailsEditViewModel Create(object model, Type type);
    }
}
