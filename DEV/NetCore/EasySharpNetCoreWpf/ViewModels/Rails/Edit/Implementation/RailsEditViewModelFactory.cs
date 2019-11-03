using EasySharpWpf.ViewModels.Rails.Edit.Core;
using System;
using System.Windows.Data;
using EasySharpStandardMvvm.ViewModels.Rails.Edit.Core;
using EasySharpStandardMvvm.Views.Layouts.ViewModels.Rails.Core;
using EasySharpStandardMvvm.Views.Layouts.ViewModels.Rails.Edit.Core;

namespace EasySharpWpf.ViewModels.Rails.Edit.Implementation
{
    internal class RailsEditViewModelFactory : IRailsEditViewModelFactory
    {
        private readonly IRailsBindCreator railsBindCreator = new RailsBindCreator();

        public IRailsBindCreator<Binding> RailsBindCreator => railsBindCreator;

        public IRailsEditViewModel Create(object model, Type type = null)
        {
            return new RailsEditViewModel(model, type);
        }
    }
}
