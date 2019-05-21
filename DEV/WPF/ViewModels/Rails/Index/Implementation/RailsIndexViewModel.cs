using EasySharpStandard.Collections.Core;
using EasySharpWpf.ViewModels.Rails.Core.Edit;
using EasySharpWpf.ViewModels.Rails.Core.Index;
using EasySharpWpf.ViewModels.Rails.Edit.Core;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;

namespace EasySharpWpf.ViewModels.Rails.Implementation.Index
{
    internal class RailsIndexViewModel : ViewModelBase, IRailsIndexViewModel
    {
        private readonly IRailsEditViewModelFactory railsEditViewModelFactory;

        public RailsIndexViewModel(
            IList modelList,
            Type type,
            IRailsEditViewModelFactory railsEditViewModelFactory = null)
        {
            this.railsEditViewModelFactory = railsEditViewModelFactory.Resolve();

            this.ItemsSource = new ObservableModelLinkedCollection<IRailsEditViewModel>(
                modelList.ToEnumerable().Select(this.railsEditViewModelFactory.Create),
                modelList);
            this.ItemType = type;
        }

        public ObservableCollection<IRailsEditViewModel> ItemsSource { get; }

        public Type ItemType { get; }
    }
}
