using Xamarin.Forms;

namespace EasySharpXamarinForms.Views.Extensions
{
    public static class GridExtensions
    {
        public static void AddChild(
            this Grid grid,
            View view,
            int row = 0,
            int column = 0,
            double thickness = 10)
        {
            grid.Children.Add(view);
            view.SetValue(Grid.RowProperty, row);
            view.SetValue(Grid.ColumnProperty, column);
            view.SetValue(View.MarginProperty, new Thickness(thickness));
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
