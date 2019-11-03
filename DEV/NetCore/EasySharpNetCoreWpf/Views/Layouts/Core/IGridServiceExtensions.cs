using EasySharpWpf.Views.Layouts.Implementation;

namespace EasySharpWpf.Views.Layouts.Core
{
    public static class GridServiceExtensions
    {
        public static IGridService Resolve(this IGridService gridService)
        {
            return gridService ?? new GridService();
        }
    }
}
