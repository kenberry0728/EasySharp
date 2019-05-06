using EasySharpStandard.Reflections.Core;
using EasySharpWpf.ViewModels.Core;
using EasySharpWpf.ViewModels.Rails.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasySharpWpf.ViewModels.Rails.Core.Edit
{
    public class RailsEditViewModel<T> : ViewModelBase, IViewModelWithModel<T>
    {
        public RailsEditViewModel(T model)
        {
            this.Model = model;
        }

        public T Model { get; }
    }
}
