using EasySharpStandard.Collections.Core;
using EasySharpStandardMvvm.ViewModels.Core;
using EasySharpStandardMvvm.ViewModels.Rails.Edit.Core;
using EasySharpStandardMvvm.ViewModels.Rails.Index.Core;
using EasySharpWpf.ViewModels.Rails.Edit.Core;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;

namespace EasySharpWpf.ViewModels.Rails.Implementation.Index
{
    internal class RailsIndexViewModel : RailsIndexViewModelBase, IRailsIndexViewModel
    {
        private readonly IRailsEditViewModelFactory<Binding> railsEditViewModelFactory;

        public RailsIndexViewModel(
            IList modelList,
            Type type,
            IRailsEditViewModelFactory railsEditViewModelFactory = null)
        {
            this.railsEditViewModelFactory = railsEditViewModelFactory.Resolve();

            this.ItemsSource = new ObservableModelLinkedCollection<IRailsEditViewModel>(
                modelList.ToEnumerable().Select(m => this.railsEditViewModelFactory.Create(m)),
                modelList);
            this.ItemType = type;
        }
    }
}
