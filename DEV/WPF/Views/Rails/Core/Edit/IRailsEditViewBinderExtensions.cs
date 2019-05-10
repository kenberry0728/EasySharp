using EasySharpWpf.Views.Rails.Core.Edit.Interfaces;
using EasySharpWpf.Views.Rails.Implementations;

namespace EasySharpWpf.Views.Rails.Core.Edit
{
    public static class IRailsEditViewBinderExtensions
    {
        public static IRailsEditViewBinder Resolve(this IRailsEditViewBinder viewBinder)
        {
            return viewBinder ?? new RailsEditViewBinder();
        }
    }
}
