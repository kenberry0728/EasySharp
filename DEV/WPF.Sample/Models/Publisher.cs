namespace EasySharpWpf.Sample.Models
{
    public class Publisher
    {
        public string Name { get; set; }

        public PublisherType PublisherType { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
