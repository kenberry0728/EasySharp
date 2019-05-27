using EasySharpWpf.ViewModels.Rails.Core.Edit;
using System;

namespace EasySharpXamarinForms.ViewModels.Rails.Edit.Core
{
    public interface IRailsEditViewModelFactory
    {
        IRailsBindCreator RailsBindCreator { get; }

        IRailsEditViewModel Create(object model);

        IRailsEditViewModel Create(object model, Type type);
    }
}
