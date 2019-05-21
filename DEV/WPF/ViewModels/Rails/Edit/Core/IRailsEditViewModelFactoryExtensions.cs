using EasySharpWpf.ViewModels.Rails.Edit.Implementation;

namespace EasySharpWpf.ViewModels.Rails.Edit.Core
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
