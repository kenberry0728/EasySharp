using EasySharpWpf.Views.Rails.Core.Edit.Interfaces;
using EasySharpWpf.Views.Rails.Implementations;

namespace EasySharpWpf.Views.Rails.Core.Edit
{
    public static class IRailsEditViewBinderExtensions
    {
        public static IRailsEditViewBinder<T> Resolve<T>(this IRailsEditViewBinder<T> viewBinder)
        {
            return viewBinder ?? new RailsEditViewBinder<T>();
        }
    }
}
