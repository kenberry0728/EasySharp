using EasySharpWpf.Views.Rails.Core.Edit;
using EasySharpWpf.Views.Rails.Core.Edit.Interfaces;

namespace EasySharpWpf.Views.EasyViews.Core
{
    public static class IRailsEditViewFactoryExtensions
    {
        public static IRailsEditViewFactory2 Resolve(this IRailsEditViewFactory2 factory)
        {
            return factory ?? new DefaultRailsEditViewFactory2();
        }
    }
}
