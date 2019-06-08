using CommonLibrary.TreeData.TreeStructure;

namespace EasySharpStandard.DataStructures.Trees.Core.Models
{
    public class IndentedString : IIndentedItem<string>
    {
        public IndentedString(string tabbedString)
        {
            this.Content = tabbedString.TrimStart('\t');
            this.Depth = tabbedString.Length - this.Content.Length;
        }

        public string Content { get; }

        public int Depth { get; }
    }
}
