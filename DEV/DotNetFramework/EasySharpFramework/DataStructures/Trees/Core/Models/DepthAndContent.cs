namespace EasySharpStandard.DataStructures.Trees.Core.Models
{
    public class IndentedItem<T> : IIndentedItem<T>
    {
        public IndentedItem(int depth, T content)
        {
            this.Depth = depth;
            this.Content = content;
        }

        public int Depth { get; }

        public T Content { get; }
    }
}
