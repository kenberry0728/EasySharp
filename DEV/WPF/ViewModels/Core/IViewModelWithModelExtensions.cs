using System.Reflection;

namespace EasySharpWpf.ViewModels.Core
{
    public static class IViewModelWithModelExtensions
    {
        public static string GetBindingPathOnViewModelWithModel<T>(
            this MemberInfo propertyInfo)
        {
            IViewModelWithModel<T> viewModel;
            return nameof(viewModel.Model) + "." + propertyInfo.Name;
        }

        //public static string GetBindingPath<TModel>(
        //    this IViewModelWithModel<TModel> viewModel,
        //    MemberInfo propertyInfo)
        //{
        //    return nameof(viewModel.Model) + "." + propertyInfo.Name;
        //}
    }
}
