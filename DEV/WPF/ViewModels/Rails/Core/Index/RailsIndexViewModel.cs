using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace EasySharpWpf.ViewModels.Rails.Core.Index
{
    public class RailsIndexViewModel<T> : ViewModelBase
    {
        public RailsIndexViewModel(List<T> modelList)
        {
            this.ItemsSource = new ObservableModelLinkedCollection<RailsItemViewModel<T>, T>(
                modelList.Select(m => new RailsItemViewModel<T>(m)), 
                modelList);
        }

        public ObservableCollection<RailsItemViewModel<T>> ItemsSource { get; }
    }
}
