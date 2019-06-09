using System;
using System.Collections.ObjectModel;
using EasySharpStandardMvvm.Views.Layouts.ViewModels.Core;
using EasySharpStandardMvvm.Views.Layouts.ViewModels.Rails.Edit.Core;

namespace EasySharpStandardMvvm.Views.Layouts.ViewModels.Rails.Index.Core
{
    public class RailsIndexViewModelBase : ViewModelBase, IRailsIndexViewModel
    {
        public ObservableCollection<IRailsEditViewModel> ItemsSource { get; protected set; }

        public Type ItemType { get; protected set; }
    }
}
