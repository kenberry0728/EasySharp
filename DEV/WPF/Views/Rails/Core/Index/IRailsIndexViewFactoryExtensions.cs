using EasySharpWpf.Views.Rails.Core.Index.Interfaces;

namespace EasySharpWpf.Views.Rails.Core.Index
{
    public static class IRailsIndexViewFactoryExtensions
    {
        public static IRailsIndexViewFactory Resolve(this IRailsIndexViewFactory factory)
        {
            return factory ?? new DefaultRailsIndexViewFactory();
        }
    }
}
