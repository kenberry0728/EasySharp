using EasySharpWpf.Views.Layouts.Implementation;

namespace EasySharpWpf.Views.Layouts.Core
{
    public static class IGridServiceExtensions
    {
        public static IGridService Resolve(this IGridService gridService)
        {
            return gridService ?? new GridService();
        }
    }
}
