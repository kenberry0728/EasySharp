using EasySharpXamarinForms.Views.Layouts.Implementation;

namespace EasySharpXamarinForms.Views.Layouts.Core
{
    public static class IGridServiceExtensions
    {
        public static IGridService Resolve(this IGridService gridService)
        {
            return gridService ?? new GridService();
        }
    }
}
