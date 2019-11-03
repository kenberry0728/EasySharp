using System;
using System.Collections;
using System.Linq;
using EasySharpStandard.Collections.Core;
using EasySharpStandardMvvm.Views.Layouts.ViewModels.Core;
using EasySharpStandardMvvm.Views.Layouts.ViewModels.Rails.Edit.Core;
using EasySharpStandardMvvm.Views.Layouts.ViewModels.Rails.Index.Core;
using EasySharpWpf.ViewModels.Rails.Edit.Core;

namespace EasySharpWpf.ViewModels.Rails.Index.Implementation
{
    internal class RailsIndexViewModel : RailsIndexViewModelBase
    {
        public RailsIndexViewModel(
            IList modelList,
            Type type,
            IRailsEditViewModelFactory railsEditViewModelFactory = null)
        {
            railsEditViewModelFactory = railsEditViewModelFactory.Resolve();
            this.ItemsSource = new ObservableModelLinkedCollection<IRailsEditViewModel>(
                modelList.ToEnumerable().Select(m => railsEditViewModelFactory.Create(m)),
                modelList);
            this.ItemType = type;
        }
    }
}
