using EasySharpWpf.ViewModels.Rails.Core.Edit;
using System;

namespace EasySharpWpf.ViewModels.Rails.Edit.Core
{
    public interface IRailsEditViewModelFactory
    {
        IRailsEditViewModel Create(object model, Type type = null);
    }
}
