using EasySharpStandardMvvm.ViewModels.Rails.Core;
using EasySharpWpf.ViewModels.Rails.Core.Edit;
using EasySharpWpf.ViewModels.Rails.Edit.Core;
using System;
using System.Windows.Data;

namespace EasySharpWpf.ViewModels.Rails.Edit.Implementation
{
    internal class RailsEditViewModelFactory : IRailsEditViewModelFactory<Binding>
    {
        public IRailsBindCreator<Binding> RailsBindCreator { get; } = new RailsBindCreator();

        public IRailsEditViewModel Create(object model)
        {
            return this.Create(model, null);
        }

        public IRailsEditViewModel Create(object model, Type type)
        {
            return new RailsEditViewModel(model, type);
        }
    }
}
