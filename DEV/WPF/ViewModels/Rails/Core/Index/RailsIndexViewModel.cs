using EasySharpWpf.ViewModels.Rails.Core.Edit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace EasySharpWpf.ViewModels.Rails.Core.Index
{
    public class RailsIndexViewModel : ViewModelBase
    {
        public RailsIndexViewModel(IList modelList, Type type)
        {
            this.ItemsSource = new ObservableModelLinkedCollection2<RailsEditViewModel2>(
                modelList.OfType<object>().Select(m => new RailsEditViewModel2(m)), 
                modelList);
            this.Type = type;
        }

        public ObservableCollection<RailsEditViewModel2> ItemsSource { get; }
        public Type Type { get; }
    }
}
