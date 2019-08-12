using System;
using System.Collections.Generic;
using System.Linq;

namespace EasySharpStandard.DataStructures.Trees.Core.Models
{
    public static class TreeItemExtension
    {
        public static void CreateTree<T>(this TreeItem<T> parent, Func<T, IEnumerable<T>> getChildren)
        {
            var children = getChildren(parent.Item).Select(c => new TreeItem<T>(c)).ToList();
            parent.AddChildren(children);
            foreach (var child in children)
            {
                child.CreateTree(getChildren);
            }
        }
    }
}
