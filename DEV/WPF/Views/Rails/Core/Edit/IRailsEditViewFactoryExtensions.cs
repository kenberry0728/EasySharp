using EasySharpWpf.Views.Rails.Core.Edit;

namespace EasySharpWpf.Views.EasyViews.Core
{
    public static class IRailsEditViewFactoryExtensions
    {
        public static IRailsEditViewFactory<T> Resolve<T>(this IRailsEditViewFactory<T> factory)
        {
            return factory ?? new DefaultRailsEditViewFactory<T>();
        }
    }
}
