using EasySharpStandardMvvm.ViewModels.Rails.Edit.Core;
using EasySharpWpf.ViewModels.Rails.Edit.Core;
using System;
using Xamarin.Forms;

namespace EasySharpXamarinForms.ViewModels.Rails.Edit.Core
{
    public interface IRailsEditViewModelFactory
    {
        IRailsBindCreator<Binding> RailsBindCreator { get; }

        IRailsEditViewModel Create(object model, Type type = null);
    }
}
