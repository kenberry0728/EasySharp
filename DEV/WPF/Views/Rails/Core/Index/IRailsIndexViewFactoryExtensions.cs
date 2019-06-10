using EasySharpWpf.ViewModels.Rails.Edit.Core;
using EasySharpWpf.Views.Layouts.Core;
using EasySharpWpf.Views.Rails.Core.Edit.Interfaces;
using EasySharpWpf.Views.Rails.Core.Index.Interfaces;

namespace EasySharpWpf.Views.Rails.Core.Index
{
    public static class RailsIndexViewFactoryExtensions
    {
        public static IRailsIndexViewFactory Resolve(
            this IRailsIndexViewFactory factory,
            IRailsEditViewFactory editViewFactory = null,
            IRailsEditViewModelFactory railsEditViewModelFactory = null,
            IGridService gridService = null)
        {
            return factory 
                   ?? new DefaultRailsIndexViewFactory(editViewFactory, railsEditViewModelFactory, gridService);
        }
    }
}
