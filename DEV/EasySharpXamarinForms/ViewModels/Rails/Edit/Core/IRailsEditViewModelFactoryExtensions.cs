using EasySharpXamarinForms.ViewModels.Rails.Edit.Implementation;

namespace EasySharpXamarinForms.ViewModels.Rails.Edit.Core
{
    public static class IRailsEditViewModelFactoryExtensions
    {
        public static IRailsEditViewModelFactory Resolve(
            this IRailsEditViewModelFactory railsEditViewModelFactory)
        {
            return railsEditViewModelFactory ?? new RailsEditViewModelFactory();
        }
    }
}
