namespace EasySharp.Location
{
    public class TextLocation : LineLocation
    {
        public TextLocation(int row = -1, int column = -1, int length = 0)
            : base(column, length)
        {
            this.Row = row;
        }

        public int Row { get; private set; }

        public override string ToString()
        {
            return $"{nameof(this.Row)}={this.Row}, {base.ToString()}";
        }
    }
}
