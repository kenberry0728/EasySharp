using EasySharpWpf.Views.Rails.Core.Edit;
using EasySharpWpf.Views.Rails.Core.Index.Interfaces;

namespace EasySharpWpf.Views.Rails.Core.Index
{
    public static class IRailsIndexViewFactoryExtensions
    {
        public static IRailsIndexViewFactory Resolve(this IRailsIndexViewFactory factory, IRailsEditViewFactory editViewFactory = null)
        {
            return factory ?? new DefaultRailsIndexViewFactory(editViewFactory);
        }
    }
}
