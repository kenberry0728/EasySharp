﻿using System.Windows;
using System.Windows.Controls;

namespace EasySharpWpf.Views.Extensions
{
    public static class GridExtensions
    {
        public static void AddChild(
            this Grid grid, 
            UIElement uiElement, 
            int row, 
            int column, 
            double thickness = 10)
        {
            grid.Children.Add(uiElement);
            uiElement.SetValue(Grid.RowProperty, row);
            uiElement.SetValue(Grid.ColumnProperty, column);
            uiElement.SetValue(FrameworkElement.MarginProperty, new Thickness(thickness));
        }

        public static void AddColumnDefinition(this Grid grid, GridLength gridLength)
        {
            grid.ColumnDefinitions.Add(
                new ColumnDefinition()
                {
                    Width = gridLength
                });
        }

        public static void AddRowDefinition(this Grid grid, GridLength gridLength)
        {
            grid.RowDefinitions.Add(
                new RowDefinition()
                {
                    Height = gridLength
                });
        }
    }
}
