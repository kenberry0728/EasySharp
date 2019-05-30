using EasySharpStandardMvvm.ViewModels.Rails.Core;
using System;

namespace EasySharpWpf.ViewModels.Rails.Edit.Core
{
    public interface IRailsEditViewModelFactory<TBinding>
    {
        IRailsBindCreator<TBinding> RailsBindCreator { get; }

        IRailsEditViewModel Create(object model);

        IRailsEditViewModel Create(object model, Type type);
    }
}
