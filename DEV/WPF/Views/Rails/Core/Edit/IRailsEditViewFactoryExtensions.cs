using EasySharpWpf.Views.Rails.Core.Edit;
using EasySharpWpf.Views.Rails.Core.Edit.Interfaces;

namespace EasySharpWpf.Views.EasyViews.Core
{
    public static class IRailsEditViewFactoryExtensions
    {
        public static IRailsEditViewFactory<T> Resolve<T>(this IRailsEditViewFactory<T> factory)
            where T: class, new()
        {
            return factory ?? new DefaultRailsEditViewFactory<T>();
        }
    }
}
