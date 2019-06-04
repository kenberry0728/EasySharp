using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using EasySharpStandardMvvm.ViewModels.Rails.Edit.Core;
using EasySharpStandardMvvm.ViewModels.Rails.Index.Core.Interfaces;
using EasySharpWpf.Views.Layouts.Core;

namespace EasySharpWpf.Views.Rails.Core.Edit
{
    public abstract class DefaultRailsEditViewFactoryBase 
        : DefaultRailsEditViewFactoryBase<Binding, UIElement, Grid>, IRailsEditViewFactory
    {
        public DefaultRailsEditViewFactoryBase(
            IRailsIndexViewFactory<UIElement> railsIndexViewFactory, 
            IRailsEditViewModelFactory<Binding> railsEditViewModelFactory,
            IGridService gridService) 
            : base(railsIndexViewFactory, railsEditViewModelFactory, gridService)
        {
        }
    }
}
