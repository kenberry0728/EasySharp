using EasySharpStandardMvvm.ViewModels.Rails.Core;
using EasySharpWpf.ViewModels.Rails.Edit.Core;
using System;
using Xamarin.Forms;

namespace EasySharpXamarinForms.Sample.ViewModels.Rails.Edit.Core
{
    public interface IRailsEditViewModelFactory
    {
        IRailsBindCreator<Binding> RailsBindCreator { get; }

        IRailsEditViewModel Create(object model);

        IRailsEditViewModel Create(object model, Type type);
    }
}
