using System;
using System.Collections.ObjectModel;
using EasySharpStandardMvvm.ViewModels.Rails.Edit.Core;

namespace EasySharpStandardMvvm.ViewModels.Rails.Index.Core
{
    public interface IRailsIndexViewModel
    {
        ObservableCollection<IRailsEditViewModel> ItemsSource { get; }
        Type ItemType { get; }
    }
}