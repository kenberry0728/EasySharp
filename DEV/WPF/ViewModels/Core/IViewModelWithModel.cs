namespace EasySharpWpf.ViewModels.Core
{
    public interface IViewModelWithModel<T>
    {
        T Model { get; }
    }
}
