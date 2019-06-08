using EasySharpStandardMvvm.Views.Rails.Edit.Core;
using EasySharpXamarinForms.ViewModels.Rails.Edit.Core;
using EasySharpXamarinForms.Views.Layouts.Core;
using EasySharpXamarinForms.Views.Rails.Core.Edit.Interfaces;
using EasySharpXamarinForms.Views.Rails.Core.Index.Interfaces;
using Xamarin.Forms;

namespace EasySharpXamarinForms.Views.Rails.Core.Edit
{
    public abstract class DefaultRailsEditViewFactoryBase :
        DefaultRailsEditViewFactoryBase<Binding, View, Grid>,
        IRailsEditViewFactory
    {
        protected DefaultRailsEditViewFactoryBase(
            IRailsIndexViewFactory railsIndexViewFactory,
            IRailsEditViewModelFactory railsEditViewModelFactory,
            IGridService gridService)
            : base(railsIndexViewFactory, railsEditViewModelFactory, gridService)
        {
        }
    }
}
