using EasySharpXamarinForms.Views.Rails.Core.Edit.Interfaces;
using EasySharpXamarinForms.Views.Rails.Core.Index.Interfaces;

namespace EasySharpXamarinForms.Views.Rails.Core.Index
{
    public static class IRailsIndexViewFactoryExtensions
    {
        public static IRailsIndexViewFactory Resolve(this IRailsIndexViewFactory factory, IRailsEditViewFactory editViewFactory = null)
        {
            return factory;// ?? new DefaultRailsIndexViewFactory(editViewFactory);
        }
    }
}
