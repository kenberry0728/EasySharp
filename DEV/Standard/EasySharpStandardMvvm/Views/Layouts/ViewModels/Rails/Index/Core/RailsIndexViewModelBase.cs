using System;
using System.Collections.ObjectModel;
using EasySharpStandardMvvm.Views.Layouts.ViewModels.Core;
using EasySharpStandardMvvm.Views.Layouts.ViewModels.Rails.Edit.Core;

namespace EasySharpStandardMvvm.Views.Layouts.ViewModels.Rails.Index.Core
{
    public class RailsIndexViewModelBase : ViewModelBase, IRailsIndexViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "protected set is needed not to raise collectionchanged event?")]
        public ObservableCollection<IRailsEditViewModel> ItemsSource { get; protected set; }

        public Type ItemType { get; protected set; }
    }
}
