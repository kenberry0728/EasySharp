using EasySharpStandardMvvm.ViewModels.Rails.Edit.Core;
using EasySharpXamarinForms.Views.Rails.Core.Edit.Interfaces;
using System;

namespace EasySharpXamarinForms.ViewModels.Rails.Edit.Core
{
    public interface IRailsEditViewModelFactory
    {
        IRailsBindCreator RailsBindCreator { get; }

        IRailsEditViewModel Create(object model, Type type = null);
    }
}
