using System.Runtime.Serialization;

namespace EasySharpStandard.Locations.Core.Models
{
    [DataContract(Namespace = "")]
    public class LineLocation : ILocation
    {
        public LineLocation(int column, int length = 0)
        {
            this.Column = column;
            this.Width = length;
        }

        [DataMember]
        public int Column { get; private set; }

        [DataMember]
        public int Width { get; private set; }

        public string LocationText => this.ToString();

        public override string ToString()
        {
            return $"{nameof(this.Column)}={this.Column}, {nameof(this.Width)}={this.Width}";
        }
    }
}
