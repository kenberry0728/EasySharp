using System;

namespace EasySharpStandardMvvm.ViewModels.Core
{
    public interface IRailsEditViewModel : IViewModelWithModel
    {
        string Content { get; }

        Type Type { get; }

        object this[string propertyName] { get; set; }
    }
}