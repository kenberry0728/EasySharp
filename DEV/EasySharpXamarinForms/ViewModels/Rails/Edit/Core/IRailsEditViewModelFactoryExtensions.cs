using EasySharpXamarinForms.ViewModels.Rails.Edit.Implementation;

namespace EasySharpXamarinForms.ViewModels.Rails.Edit.Core
{
    public static class RailsEditViewModelFactoryExtensions
    {
        public static IRailsEditViewModelFactory Resolve(
            this IRailsEditViewModelFactory railsEditViewModelFactory)
        {
            return railsEditViewModelFactory ?? new RailsEditViewModelFactory();
        }
    }
}
