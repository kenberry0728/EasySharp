using EasySharpStandard.Collections.Core;
using EasySharpWpf.ViewModels.Rails.Core.Edit;
using EasySharpWpf.ViewModels.Rails.Core.Index;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;

namespace EasySharpWpf.ViewModels.Rails.Implementation.Index
{
    internal class RailsIndexViewModel : ViewModelBase, IRailsIndexViewModel
    {
        public RailsIndexViewModel(IList modelList, Type type)
        {
            this.ItemsSource = new ObservableModelLinkedCollection<IRailsEditViewModel>(
                modelList.ToEnumerable().Select(m => new RailsEditViewModel(m)),
                modelList);
            this.Type = type;
        }

        public ObservableCollection<IRailsEditViewModel> ItemsSource { get; }

        public Type Type { get; }
    }
}
