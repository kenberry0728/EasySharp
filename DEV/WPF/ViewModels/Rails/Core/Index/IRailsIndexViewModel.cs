using System;
using System.Collections.ObjectModel;
using EasySharpWpf.ViewModels.Rails.Core.Edit;

namespace EasySharpWpf.ViewModels.Rails.Core.Index
{
    public interface IRailsIndexViewModel
    {
        ObservableCollection<IRailsEditViewModel> ItemsSource { get; }
        Type Type { get; }
    }
}