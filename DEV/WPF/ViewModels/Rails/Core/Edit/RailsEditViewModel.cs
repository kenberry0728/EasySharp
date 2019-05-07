using EasySharpWpf.ViewModels.Core;

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
