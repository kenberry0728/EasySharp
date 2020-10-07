using EasySharp;
using EasySharpXamarinForms.Views.Layouts.Core;
using Xamarin.Forms;

namespace EasySharpXamarinForms.Views.Layouts.Implementation
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "TODO: ValueObjectにしたほうがうまくいくかも。")]
    public class GridService : IGridService
    {
        public void AddChild(
            Grid grid,
            View view,
            int row = 0,
            int column = 0,
            double thickness = 10)
        {
            #pragma warning disable CA1062 // Validate arguments of public methods
            grid.ThrowArgumentExceptionIfNull(nameof(grid));
            view.ThrowArgumentExceptionIfNull(nameof(view));
            grid.Children.Add(view);
            view.SetValue(Grid.RowProperty, row);
            #pragma warning restore CA1062 // Validate arguments of public methods

            view.SetValue(Grid.ColumnProperty, column);
            view.SetValue(View.MarginProperty, new Thickness(thickness));
        }

        public void AddColumnDefinition(Grid grid, double width)
        {
            grid.ColumnDefinitions.Add(
                new ColumnDefinition
                {
                    Width = new GridLength(width)
                });
        }

        public void AddRowDefinition(Grid grid)
        {
            grid.RowDefinitions.Add(new RowDefinition());
        }

        public void AddRowDefinition(Grid grid, double height)
        {
            grid.RowDefinitions.Add(
                new RowDefinition
                {
                    Height = new GridLength(height)
                });
        }

        public void AddStarColumnDefinition(Grid grid, double ratio = 1)
        {
            throw new System.NotImplementedException();
        }

        public void AddStarRowDefinition(Grid grid, double ration = 1)
        {
        }

        public Grid Create(object viewModel)
        {
            return new Grid { BindingContext = viewModel };
        }

        public void AddAutoColumnDefinition(Grid grid)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        }

        public void AddAutoRowDefinition(Grid grid)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        }
    }
}
