using EasySharpWpf.Views.Rails.Core.Edit.Interfaces;
using EasySharpWpf.Views.Rails.Core.Index.Interfaces;

namespace EasySharpWpf.Views.Rails.Core.Edit
{
    public static class RailsEditViewFactoryExtensions
    {
        public static IRailsEditViewFactory Resolve(this IRailsEditViewFactory factory, IRailsIndexViewFactory indexFactory = null)
        {
            return factory ?? new DefaultRailsEditViewFactory(indexFactory);
        }
    }
}
