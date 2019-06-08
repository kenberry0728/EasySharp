using EasySharpXamarinForms.Views.Layouts.Implementation;

namespace EasySharpXamarinForms.Views.Layouts.Core
{
    public static class GridServiceExtensions
    {
        public static IGridService Resolve(this IGridService gridService)
        {
            return gridService ?? new GridService();
        }
    }
}
