using EasySharpXamarinForms.Views.Rails.Core.Edit.Interfaces;
using EasySharpXamarinForms.Views.Rails.Core.Index.Interfaces;

namespace EasySharpXamarinForms.Views.Rails.Core.Edit
{
    public static class IRailsEditViewFactoryExtensions
    {
        public static IRailsEditViewFactory Resolve(this IRailsEditViewFactory factory, IRailsIndexViewFactory indexFactory = null)
        {
            return factory ?? new DefaultRailsEditViewFactory(indexFactory);
        }
    }
}
