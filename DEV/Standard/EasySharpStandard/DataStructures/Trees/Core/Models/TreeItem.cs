using System.Collections.Generic;
using System.Linq;

namespace EasySharp.DataStructures.Trees.Core.Models
{
    public class TreeItem<T> : ITreeItem<TreeItem<T>>
    {
        private TreeItem<T> parent;
        private readonly List<TreeItem<T>> children = new List<TreeItem<T>>();

        public TreeItem(T item)
        {
            this.Item = item;
        }

        public T Item { get; }


        public bool IsRoot => this.parent == null;

        public bool IsLeaf => !this.children.Any();

        public virtual void AddChild(TreeItem<T> child)
        {
            child.ThrowArgumentExceptionIfNull(nameof(child));

            child.parent = this;
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
            return this.parent;
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
                    return this.parent.Depth + 1;
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
