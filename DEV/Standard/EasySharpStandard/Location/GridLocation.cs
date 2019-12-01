using System;

namespace EasySharp.Location
{
    public struct GridLocation : ILocation, IEquatable<GridLocation>
    {
        public GridLocation(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        public int Row { get; set; }

        public int Column { get; set; }

        public override string ToString()
        {
            return $"Row={this.Row}, Column={this.Column}";
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(GridLocation left, GridLocation right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GridLocation left, GridLocation right)
        {
            return !(left == right);
        }

        public bool Equals(GridLocation other)
        {
            return this.Row == other.Row
                && this.Column == other.Column;
        }
    }
}
