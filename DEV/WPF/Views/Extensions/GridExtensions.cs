using System.Windows;
using System.Windows.Controls;

namespace EasySharpWpf.Views.Extensions
{
    public static class GridExtensions
    {
        public static void AddChild(
            this Grid grid, 
            UIElement uiElement, 
            int row = 0, 
            int column = 0, 
            double thickness = 10)
        {
            grid.Children.Add(uiElement);
            uiElement.SetValue(Grid.RowProperty, row);
            uiElement.SetValue(Grid.ColumnProperty, column);
            uiElement.SetValue(FrameworkElement.MarginProperty, new Thickness(thickness));
        }

        public static void AddColumnDefinition(this Grid grid, GridLength width)
        {
            grid.ColumnDefinitions.Add(
                new ColumnDefinition()
                {
                    Width = width
                });
        }

        public static void AddRowDefinition(this Grid grid)
        {
            grid.RowDefinitions.Add(new RowDefinition());
        }

        public static void AddRowDefinition(this Grid grid, GridLength height)
        {
            grid.RowDefinitions.Add(
                new RowDefinition()
                {
                    Height = height
                });
        }
    }
}
