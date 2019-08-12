using System.Runtime.Serialization;

namespace EasySharpStandard.Locations.Core.Models
{
    [DataContract(Namespace = "")]
    public class TextLocation : LineLocation
    {
        public TextLocation(int column = -1, int row = -1, int length = 0)
            : base(column, length)
        {
            this.Row = row;
        }

        [DataMember]
        public int Row { get; private set; }

        public override string ToString()
        {
            return $"{nameof(this.Row)}={this.Row}, {base.ToString()}";
        }
    }
}
