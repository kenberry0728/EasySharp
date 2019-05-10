using EasySharpWpf.Views.Rails.Core.Edit;

namespace EasySharpWpf.Views.EasyViews.Core
{
    public static class IRailsEditViewFactoryExtensions
    {
        public static IRailsEditViewFactory Resolve(this IRailsEditViewFactory factory)
        {
            return factory ?? new DefaultRailsEditViewFactory();
        }
    }
}
