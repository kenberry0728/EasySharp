using System;
using System.Collections;
using System.Linq;
using EasySharpStandard.Collections.Core;
using EasySharpStandardMvvm.ViewModels.Core;
using EasySharpStandardMvvm.ViewModels.Rails.Edit.Core;
using EasySharpStandardMvvm.ViewModels.Rails.Index.Core;
using EasySharpStandardMvvm.Views.Layouts.ViewModels.Rails.Index.Core;
using EasySharpXamarinForms.ViewModels.Rails.Edit.Core;

namespace EasySharpXamarinForms.ViewModels.Rails.Index.Implementation
{
    internal class RailsIndexViewModel : RailsIndexViewModelBase, IRailsIndexViewModel
    {
        private readonly IRailsEditViewModelFactory railsEditViewModelFactory;

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
