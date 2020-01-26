using EasySharpNetCoreWpf.Views;

namespace EasySharpNetCoreWpf.ViewModels
{
    public interface IWindowViewModel
    {
        IWindow TargetWindow { get; set; }

        bool CanCloseWindow(object arg);
    }
}
