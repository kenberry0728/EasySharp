using EasySharpStandardMvvm.ViewModels.Rails.Edit.Core;
using EasySharpWpf.ViewModels.Rails.Core.Edit;
using EasySharpXamarinForms.Sample.ViewModels.Rails.Edit.Implementation;
using EasySharpXamarinForms.ViewModels.Rails.Edit.Core;
using EasySharpXamarinForms.Views.Rails.Core.Edit.Interfaces;
using System;

namespace EasySharpXamarinForms.ViewModels.Rails.Edit.Implementation
{
    internal class RailsEditViewModelFactory : IRailsEditViewModelFactory
    {
        public IRailsBindCreator RailsBindCreator { get; } = new RailsBindCreator();

        public IRailsEditViewModel Create(object model, Type type = null)
        {
            return new RailsEditViewModel(model, type);
        }
    }
}
