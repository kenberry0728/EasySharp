using System.Windows;
using System.Windows.Data;
using EasySharpStandardMvvm.ViewModels.Rails.Edit.Core;
using EasySharpStandardMvvm.ViewModels.Rails.Index.Core.Interfaces;

namespace EasySharpWpf.Views.Rails.Core.Edit
{
    public abstract class DefaultRailsEditViewFactoryBase 
        : DefaultRailsEditViewFactoryBase<Binding, UIElement>, IRailsEditViewFactory
    {
        public DefaultRailsEditViewFactoryBase(
            IRailsIndexViewFactory<UIElement> railsIndexViewFactory, 
            IRailsEditViewModelFactory<Binding> railsEditViewModelFactory) 
            : base(railsIndexViewFactory, railsEditViewModelFactory)
        {
        }
    }
}
