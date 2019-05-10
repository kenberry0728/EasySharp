using EasySharpWpf.ViewModels.Rails.Core.Edit;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;

namespace EasySharpWpf.ViewModels.Rails.Core.Index
{
    public class RailsIndexViewModel : ViewModelBase
    {
        public RailsIndexViewModel(IList modelList, Type type)
        {
            this.ItemsSource = new ObservableModelLinkedCollection<RailsEditViewModel>(
                modelList.OfType<object>().Select(m => new RailsEditViewModel(m)), 
                modelList);
            this.Type = type;
        }

        public ObservableCollection<RailsEditViewModel> ItemsSource { get; }

        public Type Type { get; }
    }
}
