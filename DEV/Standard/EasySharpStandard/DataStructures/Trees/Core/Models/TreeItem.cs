using System.Collections.Generic;
using System.Linq;

namespace EasySharp.DataStructures.Trees.Core.Models
{
    public class TreeItem<T> : ITreeItem<TreeItem<T>>
    {
        private readonly List<TreeItem<T>> children = new List<TreeItem<T>>();

        public TreeItem(T item)
        {
            this.Item = item;
        }

        public T Item { get; }

        public TreeItem<T> Parent { get; private set; }

        public bool IsRoot => this.Parent == null;

        public bool IsLeaf => !this.children.Any();

        public virtual void AddChild(TreeItem<T> child)
        {
            child.ThrowArgumentExceptionIfNull(nameof(child));

            child.Parent = this;
            this.children.Add(child);
        }

        public void AddChildren(IEnumerable<TreeItem<T>> childrenToAdd)
        {
            if (childrenToAdd.IsNullOrEmpty())
            {
                return;
            }

            foreach (var child in childrenToAdd)
            {
                this.AddChild(child);
            }
        }

        public TreeItem<T> GetParent()
        {
            return this.Parent;
        }

        public IEnumerable<TreeItem<T>> GetChildren()
        {
            return this.children;
        }

        public int Depth
        {
            get
            {
                if (this.IsRoot)
                {
                    return 0;
                }
                else
                {
                    return this.Parent.Depth + 1;
                }
            }
        }

        public override string ToString()
        {
            return this.Item.ToString();
        }

        public IEnumerable<string> ToIndentStrings(int indent)
        {
            yield return this.ToString().Indent(indent);
            foreach (var child in this.children)
            {
                foreach (var childIndentString in child.ToIndentStrings(indent + 1))
                {
                    yield return childIndentString;
                }
            }
        }
    }
}
