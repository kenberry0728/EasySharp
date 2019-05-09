using System;
using System.Collections;
using System.Windows;

namespace EasySharpWpf.Views.Rails.Core.Index.Interfaces
{
    public interface IRailsIndexViewFactory
    {
        FrameworkElement CreateIndexView(IList model, Type type);
    }
}