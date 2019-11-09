using EasySharp.DataStructures.Trees.Core;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace EasySharpWpf.Views.VisualTrees
{
    public static class DependencyObjectExtensions
    {
        public static IEnumerable<DependencyObject> GetChildren(this DependencyObject parent)
        {
            var childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                yield return VisualTreeHelper.GetChild(parent, i);
            }
        }

        public static IEnumerable<DependencyObject> GetDescendants(this DependencyObject parent)
        {
            return parent.GetDescendants(GetChildren);
        }
    }
}
