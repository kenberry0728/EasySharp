using System;
using System.Collections.Generic;
using System.Text;

namespace EasySharp.DataStructures.Trees
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Extension")]
    public static class TreeItemExtensions
    {
        #region Public Methods

        public static T GetRootItem<T>(this T item, Func<T, T> getParent)
        {
            var parent = getParent(item);
            if (parent == null)
            {
                return item;
            }
            else
            {
                return parent.GetRootItem(getParent);
            }
        }

        public static IEnumerable<T> GetAncestors<T>(this T item, Func<T, T> getParent)
        {
            var parent = getParent(item);
            if (parent == null)
            {
                yield break;
            }

            yield return parent;
            foreach (var ancestor in parent.GetAncestors(getParent))
            {
                yield return ancestor;
            }
        }

        public static IEnumerable<T> GetAncestorsAndItself<T>(this T item, Func<T, T> getParent)
        {
            yield return item;
            foreach (var ancestor in item.GetAncestors(getParent))
            {
                yield return ancestor;
            }
        }

        public static T GetAncestor<T>(this T item, Func<T, T> getParent, Predicate<T> isTarget)
        {
            var parent = getParent(item);
            if (parent == null)
            {
                return default(T);
            }

            if (isTarget(parent))
            {
                return parent;
            }
            else
            {
                return parent.GetAncestor(getParent, isTarget);
            }
        }

        public static IEnumerable<T> GetAllItems<T>(this T item, Func<T, IEnumerable<T>> getChildren)
        {
            yield return item;
            foreach (var child in getChildren(item))
            {
                foreach (var descendant in child.GetAllItems(getChildren))
                {
                    yield return descendant;
                }
            }
        }

        public static IEnumerable<T> GetDescendants<T>(this T item, Func<T, IEnumerable<T>> getChildren)
        {
            foreach (var child in getChildren(item))
            {
                yield return child;
                foreach (var descendant in child.GetDescendants(getChildren))
                {
                    yield return descendant;
                }
            }
        }

        public static IEnumerable<T> GetItselfAndDescendants<T>(this T item, Func<T, IEnumerable<T>> getChildren)
        {
            yield return item;
            foreach (var child in getChildren(item))
            {
                yield return child;
                foreach (var descendant in child.GetDescendants(getChildren))
                {
                    yield return descendant;
                }
            }
        }

        public static string ToTreeString<T>(this T item, Func<T, string> toString, Func<T, IEnumerable<T>> getChildren)
        {
            var tab = 0;
            var sb = new StringBuilder();
            sb.Append(toString(item));
            foreach (var child in getChildren(item))
            {
                sb.Append(child.ToTreeString(toString, getChildren, tab + 1));
            }

            return sb.ToString();
        }

        public static string ToTreeString<T>(this T item, Func<T, IEnumerable<T>> getChildren)
        {
            var tab = 0;
            var sb = new StringBuilder();
            sb.Append(item.ToString());
            foreach (var child in getChildren(item))
            {
                sb.Append(child.ToTreeString(c => c.ToString(), getChildren, tab + 1));
            }

            return sb.ToString();
        }

        #endregion

        #region Private Methods

        private static string ToTreeString<T>(this T treeObject, Func<T, string> toString, Func<T, IEnumerable<T>> getChildren, int tab)
        {
            var sb = new StringBuilder();
            sb.Append(Environment.NewLine + new string('\t', tab) + toString(treeObject));
            foreach (var child in getChildren(treeObject))
            {
                sb.Append(child.ToTreeString(toString, getChildren, tab + 1));
            }

            return sb.ToString();
        }

        #endregion
    }
}
