using EasySharpStandard.Locations.Core;

namespace EasySharpStandard.DataStructures.Grids.Core.Models
{
    public struct GridLocation : ILocation
    {
        public GridLocation(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        public int Row { get; set; }

        public int Column { get; set; }

        public string LocationText => this.ToString();

        public override string ToString()
        {
            return $"Row={this.Row}, Column={this.Column}";
        }
    }
}
