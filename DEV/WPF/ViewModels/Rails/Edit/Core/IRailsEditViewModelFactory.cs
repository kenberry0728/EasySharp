using EasySharpStandardMvvm.ViewModels.Core;
using System;
using System.Windows.Data;

namespace EasySharpWpf.ViewModels.Rails.Edit.Core
{
    public interface IRailsEditViewModelFactory
    {
        IRailsBindCreator<Binding> RailsBindCreator { get; }

        IRailsEditViewModel Create(object model);

        IRailsEditViewModel Create(object model, Type type);
    }
}
