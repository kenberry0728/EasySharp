using CommonLibrary.TreeData.TreeStructure;
using EasySharpStandard.DataStructures.Trees.Core.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EasySharpStandard.DataStructures.Trees.Core
{
    public static class IndentedItemExtensions
    {
        public static IEnumerable<TreeItem<T>> ToTreeStructure<T>(this IEnumerable<IIndentedItem<T>> depthAndContents)
        {
            var stack = new Stack<TreeItem<T>>();
            var previousDepth = 0;
            var roots = new List<TreeItem<T>>();
            foreach (var depthAndContent in depthAndContents)
            {
                var treeItem = new TreeItem<T>(depthAndContent.Content);
                int depth = depthAndContent.Depth;

                if (previousDepth == depth)
                {
                    if (stack.Any())
                    {
                        stack.Pop();
                    }

                    AddToRootOrParent(stack, roots, treeItem);
                    stack.Push(treeItem);
                }
                else if (previousDepth < depth)
                {
                    Debug.Assert(depth - previousDepth == 1);

                    AddToRootOrParent(stack, roots, treeItem);
                    stack.Push(treeItem);
                }
                else
                {
                    for (int i = 0; i < previousDepth - depth; i++)
                    {
                        stack.Pop();
                    }

                    stack.Pop();
                    AddToRootOrParent(stack, roots, treeItem);
                    stack.Push(treeItem);
                }

                previousDepth = depth;
            }

            return roots;
        }

        private static void AddToRootOrParent<T>(Stack<TreeItem<T>> stack, List<TreeItem<T>> roots, TreeItem<T> treeItem)
        {
            if (stack.Any())
            {
                stack.Peek().AddChild(treeItem);
            }
            else
            {
                roots.Add(treeItem);
            }
        }
    }
}
