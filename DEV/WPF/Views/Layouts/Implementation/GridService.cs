using EasySharpWpf.Views.Layouts.Core;
using System.Windows;
using System.Windows.Controls;

namespace EasySharpWpf.Views.Layouts.Implementation
{
    public class GridService : IGridService
    {
        public void AddChild(
            Grid grid,
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

        public void AddColumnDefinition(Grid grid, double width)
        {
            grid.ColumnDefinitions.Add(
                new ColumnDefinition()
                {
                    Width = new GridLength(width)
                });
        }

        public void AddRowDefinition(Grid grid)
        {
            grid.RowDefinitions.Add(new RowDefinition());
        }

        public void AddRowDefinition(Grid grid, GridLength height)
        {
            grid.RowDefinitions.Add(
                new RowDefinition()
                {
                    Height = height
                });
        }

        public Grid Create(object viewModel)
        {
            return new Grid() { DataContext = viewModel };
        }
    }
}
