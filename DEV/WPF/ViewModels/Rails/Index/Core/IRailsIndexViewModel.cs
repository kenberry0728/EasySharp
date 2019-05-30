using System;
using System.Collections.ObjectModel;
using EasySharpStandardMvvm.ViewModels.Rails.Core;

namespace EasySharpWpf.ViewModels.Rails.Core.Index
{
    public interface IRailsIndexViewModel
    {
        ObservableCollection<IRailsEditViewModel> ItemsSource { get; }
        Type ItemType { get; }
    }
}