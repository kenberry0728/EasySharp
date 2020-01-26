using System.Windows.Input;

namespace EasySharpNetCoreWpf.Views
{
    public interface IWindow
    {
        ICommand CloseCommand { get; set; } 
    }
}
