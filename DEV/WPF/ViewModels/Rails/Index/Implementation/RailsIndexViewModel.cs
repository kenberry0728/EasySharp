using EasySharpStandard.Collections.Core;
using EasySharpStandardMvvm.ViewModels.Core;
using EasySharpStandardMvvm.ViewModels.Rails.Edit.Core;
using EasySharpWpf.ViewModels.Rails.Core.Index;
using EasySharpWpf.ViewModels.Rails.Edit.Core;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;

namespace EasySharpWpf.ViewModels.Rails.Implementation.Index
{
    internal class RailsIndexViewModel : ViewModelBase, IRailsIndexViewModel
    {
        private readonly IRailsEditViewModelFactory<Binding> railsEditViewModelFactory;

        public RailsIndexViewModel(
            IList modelList,
            Type type,
            IRailsEditViewModelFactory<Binding> railsEditViewModelFactory = null)
        {
            this.railsEditViewModelFactory = railsEditViewModelFactory.Resolve();

            this.ItemsSource = new ObservableModelLinkedCollection<IRailsEditViewModel>(
                modelList.ToEnumerable().Select(m => this.railsEditViewModelFactory.Create(m)),
                modelList);
            this.ItemType = type;
        }

        public ObservableCollection<IRailsEditViewModel> ItemsSource { get; }

        public Type ItemType { get; }
    }
}
