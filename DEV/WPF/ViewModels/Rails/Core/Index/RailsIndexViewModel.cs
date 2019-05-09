using EasySharpWpf.ViewModels.Rails.Core.Edit;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace EasySharpWpf.ViewModels.Rails.Core.Index
{
    public class RailsIndexViewModel<T> : ViewModelBase
        where T : class, new()
    {
        public RailsIndexViewModel(List<T> modelList)
        {
            this.ItemsSource = new ObservableModelLinkedCollection2<RailsEditViewModel2>(
                modelList.Select(m => new RailsEditViewModel2(m)), 
                modelList);
        }

        public ObservableCollection<RailsEditViewModel2> ItemsSource { get; }
    }
}
