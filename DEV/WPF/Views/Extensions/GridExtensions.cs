﻿using System.Windows;
using System.Windows.Controls;

namespace EasySharpWpf.Views.Extensions
{
    public static class GridExtensions
    {
        public static void AddChild(this Grid grid, UIElement uiElement, int row, int column)
        {
            grid.Children.Add(uiElement);
            uiElement.SetValue(Grid.RowProperty, row);
            uiElement.SetValue(Grid.ColumnProperty, column);
        }
    }
}
