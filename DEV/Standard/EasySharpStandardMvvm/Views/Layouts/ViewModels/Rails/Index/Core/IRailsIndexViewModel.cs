using EasySharpStandardMvvm.ViewModels.Rails.Edit.Core;
using System;
using System.Collections.ObjectModel;

namespace EasySharpStandardMvvm.ViewModels.Rails.Index.Core
{
    public interface IRailsIndexViewModel
    {
        ObservableCollection<IRailsEditViewModel> ItemsSource { get; }
        Type ItemType { get; }
    }
}