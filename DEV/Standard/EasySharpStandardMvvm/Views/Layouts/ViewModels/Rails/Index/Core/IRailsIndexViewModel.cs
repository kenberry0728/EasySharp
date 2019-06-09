using System;
using System.Collections.ObjectModel;
using EasySharpStandardMvvm.Views.Layouts.ViewModels.Rails.Edit.Core;

namespace EasySharpStandardMvvm.Views.Layouts.ViewModels.Rails.Index.Core
{
    public interface IRailsIndexViewModel
    {
        ObservableCollection<IRailsEditViewModel> ItemsSource { get; }
        Type ItemType { get; }
    }
}