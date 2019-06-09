using EasySharpXamarinForms.ViewModels.Rails.Edit.Core;
using System;
using EasySharpStandardMvvm.Views.Layouts.ViewModels.Rails.Core;
using EasySharpStandardMvvm.Views.Layouts.ViewModels.Rails.Edit.Core;
using Xamarin.Forms;

namespace EasySharpXamarinForms.ViewModels.Rails.Edit.Implementation
{
    internal class RailsEditViewModelFactory : IRailsEditViewModelFactory
    {
        public IRailsBindCreator<Binding> RailsBindCreator { get; } = new RailsBindCreator();

        public IRailsEditViewModel Create(object model, Type type = null)
        {
            return new RailsEditViewModel(model, type);
        }
    }
}
