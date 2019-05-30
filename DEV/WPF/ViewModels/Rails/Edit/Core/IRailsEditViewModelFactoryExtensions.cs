using EasySharpStandardMvvm.ViewModels.Rails.Edit.Core;
using EasySharpWpf.ViewModels.Rails.Edit.Implementation;
using System.Windows.Data;

namespace EasySharpWpf.ViewModels.Rails.Edit.Core
{
    public static class IRailsEditViewModelFactoryExtensions
    {
        public static IRailsEditViewModelFactory<Binding> Resolve(
            this IRailsEditViewModelFactory<Binding> railsEditViewModelFactory)
        {
            return railsEditViewModelFactory ?? new RailsEditViewModelFactory();
        }
    }
}
