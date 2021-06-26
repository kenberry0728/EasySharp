namespace EasySharp.DataStructures.Trees
{
    public class IndentedString : IIndentedItem<string>
    {
        public IndentedString(string tabbedString)
        {
            tabbedString.ThrowArgumentExceptionIfNull(nameof(tabbedString));

            this.Content = tabbedString.TrimStart('\t');
            this.Depth = tabbedString.Length - this.Content.Length;
        }

        public string Content { get; }

        public int Depth { get; }
    }
}
