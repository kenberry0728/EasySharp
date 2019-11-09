namespace EasySharp.Location
{
    public class LineLocation : ILocation
    {
        public LineLocation(int column, int length = 0)
        {
            this.Column = column;
            this.Length = length;
        }

        public int Column { get; private set; }

        public int Length { get; private set; }

        public override string ToString()
        {
            return $"{nameof(this.Column)}={this.Column}, {nameof(this.Length)}={this.Length}";
        }
    }
}
