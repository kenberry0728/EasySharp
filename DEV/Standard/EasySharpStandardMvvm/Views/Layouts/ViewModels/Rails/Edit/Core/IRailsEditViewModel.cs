using System;
using EasySharpStandardMvvm.Views.Layouts.ViewModels.Core;

namespace EasySharpStandardMvvm.Views.Layouts.ViewModels.Rails.Edit.Core
{
    public interface IRailsEditViewModel : IViewModelWithModel
    {
        string Content { get; }

        Type Type { get; }

        object this[string propertyName] { get; set; }
    }
}