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
                new ColumnDefinition
                {
                    Width = new GridLength(width)
                });
        }

        public void AddAutoColumnDefinition(Grid grid)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
        }

        public void AddStarColumnDefinition(Grid grid, double ratio = 1)
        {
            grid.ColumnDefinitions.Add(
                new ColumnDefinition
                {
                    Width = new GridLength(ratio, GridUnitType.Star)
                });
        }

        public void AddRowDefinition(Grid grid)
        {
            grid.RowDefinitions.Add(new RowDefinition());
        }

        public void AddRowDefinition(Grid grid, double height)
        {
            throw new System.NotImplementedException();
        }

        public void AddAutoRowDefinition(Grid grid)
        {
            grid.RowDefinitions.Add(new RowDefinition());
        }

        public void AddStarRowDefinition(Grid grid, double ratio = 1)
        {
            grid.RowDefinitions.Add(
                new RowDefinition
                {
                    Height = new GridLength(ratio, GridUnitType.Star)
                });
        }

        public Grid Create(object viewModel)
        {
            return new Grid { DataContext = viewModel };
        }
    }
}
