namespace EasySharpStandardMvvm.Views.Layouts.Core
{
    public interface IGridService<TGrid, TGridLength, TViewControl>
    {
        void AddChild(
            TGrid grid,
            TViewControl view,
            int row = 0,
            int column = 0,
            double thickness = 10);

        void AddColumnDefinition(TGrid grid, double width);

        void AddStarColumnDefinition(TGrid grid, double ratio = 1.0);

        void AddAutoColumnDefinition(TGrid grid);

        void AddRowDefinition(TGrid grid);

        void AddRowDefinition(TGrid grid, double height);

        void AddStarRowDefinition(TGrid grid, double ration = 1.0);

        void AddAutoRowDefinition(TGrid grid);

        TGrid Create(object viewModel);
    }
}
