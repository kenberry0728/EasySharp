using System.Windows.Controls;

namespace EasySharpNetCoreWpf.Views
{
    public interface ICommandTarget<T>
        where T : Control
    {
        T CommandTarget { get; set; }
    }
}
