using EasySharpStandardMvvm.ViewModels.Core;
using EasySharpStandardMvvm.ViewModels.Rails.Edit.Core;
using System;
using System.Collections.ObjectModel;

namespace EasySharpStandardMvvm.ViewModels.Rails.Index.Core
{
    public class RailsIndexViewModelBase : ViewModelBase, IRailsIndexViewModel
    {
        public ObservableCollection<IRailsEditViewModel> ItemsSource { get; protected set; }

        public Type ItemType { get; protected set; }
    }
}
