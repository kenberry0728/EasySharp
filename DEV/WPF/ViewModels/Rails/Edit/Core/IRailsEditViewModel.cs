using EasySharpWpf.ViewModels.Core;
using System;

namespace EasySharpWpf.ViewModels.Rails.Core.Edit
{
    public interface IRailsEditViewModel : IViewModelWithModel
    {
        string Content { get; }

        Type Type { get; }

        object this[string propertyName] { get; set; }
    }
}