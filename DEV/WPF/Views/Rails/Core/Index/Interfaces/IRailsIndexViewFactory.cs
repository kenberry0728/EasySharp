using System.Collections.Generic;
using System.Windows;

namespace EasySharpWpf.Views.Rails.Core.Index.Interfaces
{
    public interface IRailsIndexViewFactory<T>
    {
        FrameworkElement CreateIndexView(List<T> model);
    }
}