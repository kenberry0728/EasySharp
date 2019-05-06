namespace EasySharpWpf.Views.Rails.Core.Index
{
    public static class IRailsIndexViewFactoryExtensions
    {
        public static IRailsIndexViewFactory<T> Resolve<T>(this IRailsIndexViewFactory<T> factory)
            where T : class, new ()
        {
            return factory ?? new DefaultRailsIndexViewFactory<T>();
        }
    }
}
