using EasySharpWpf.Views.Rails.Core.Edit;
using EasySharpWpf.Views.Rails.Core.Index.Interfaces;

namespace EasySharpWpf.Views.EasyViews.Core
{
    public static class RailsEditViewFactoryExtensions
    {
        public static IRailsEditViewFactory Resolve(this IRailsEditViewFactory factory, IRailsIndexViewFactory indexFactory = null)
        {
            return factory ?? new DefaultRailsEditViewFactory(indexFactory);
        }
    }
}
