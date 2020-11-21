using EasySharp.IO;

namespace EasySharp.Location
{
    public class TextFileLocation : TextLocation
    {
        public TextFileLocation(IFilePath filePath, int row = -1, int column = -1, int length = 0)
            : base(row, column, length)
        {
            this.FilePath = filePath; ;
        }

        public IFilePath FilePath { get; }

        public override string ToString()
        {
            return $"{nameof(this.FilePath)}={this.FilePath}, {base.ToString()}";
        }
    }
}
