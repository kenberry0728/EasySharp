using EasySharpStandardMvvm.ViewModels.Core;
using System;

namespace EasySharpStandardMvvm.ViewModels.Rails.Edit.Core
{
    public interface IRailsEditViewModel : IViewModelWithModel
    {
        string Content { get; }

        Type Type { get; }

        object this[string propertyName] { get; set; }
    }
}