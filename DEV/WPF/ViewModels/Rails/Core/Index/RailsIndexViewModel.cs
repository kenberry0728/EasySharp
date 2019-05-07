using EasySharpWpf.ViewModels.Rails.Core.Edit;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace EasySharpWpf.ViewModels.Rails.Core.Index
{
    public class RailsIndexViewModel<T> : ViewModelBase
    {
        public RailsIndexViewModel(List<T> modelList)
        {
            this.ItemsSource = new ObservableModelLinkedCollection<RailsEditViewModel<T>, T>(
                modelList.Select(m => new RailsEditViewModel<T>(m)), 
                modelList);
        }

        public ObservableCollection<RailsEditViewModel<T>> ItemsSource { get; }
    }
}
