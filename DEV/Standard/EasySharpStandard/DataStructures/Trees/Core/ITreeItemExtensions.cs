using EasySharpStandard.DataStructures.Grids.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasySharpStandard.DataStructures.Trees.Core
{
    public static class TreeInterfaceItemExtensions
    {
        #region Public Methods

        public static T GetRootItem<T>(this T item)
        where T : ITreeItem<T>
        {
            return item.GetRootItem(t => t.GetParent());
        }

        public static IEnumerable<T> GetAncestors<T>(this T item)
        where T : ITreeItem<T>
        {
            return item.GetAncestors(t => t.GetParent());
        }

        public static IEnumerable<T> GetAncestorsAndItself<T>(this T item)
        where T : ITreeItem<T>
        {
            return item.GetAncestorsAndItself(t => t.GetParent());
        }

        public static T GetAncestor<T>(this T item, Predicate<T> isTarget)
        where T : ITreeItem<T>
        {
            return item.GetAncestor(t => t.GetParent(), isTarget);
        }

        public static IEnumerable<T> GetAllItems<T>(this T item)
         where T : ITreeItem<T>
        {
            return item.GetAllItems(t => t.GetChildren());
        }

        public static IEnumerable<T> GetDescendants<T>(this T item, Func<IEnumerable<T>, IEnumerable<T>> orderFunc = null)
        where T : ITreeItem<T>
        {
            return item.GetDescendants(t => orderFunc == null ? t.GetChildren() : orderFunc(t.GetChildren()));
        }

        public static IEnumerable<T> GetItselfAndDescendants<T>(this T item, Func<IEnumerable<T>, IEnumerable<T>> orderFunc = null)
        where T : ITreeItem<T>
        {
            return item.GetItselfAndDescendants(t => orderFunc == null ? t.GetChildren() : orderFunc(t.GetChildren()));
        }

        public static int GetMaxChildDepth<T>(this T item)
        where T : ITreeItem<T>
        {
            return item.GetDescendants()
                           .Where(d => d.IsLeaf)
                           .Max(x => x.Depth);
        }

        public static string ToTreeString<T>(this T item, Func<T, string> toString)
        where T : ITreeItem<T>
        {
            return item.ToTreeString(toString, t => t.GetChildren());
        }

        public static string ToTreeString<T>(this T item)
        where T : ITreeItem<T>
        {
            return item.ToTreeString(t => t.GetChildren());
        }

        public static IDictionary<GridLocation, T> GetHorizontalTableLayout<T>(
            this IEnumerable<T> roots,
            GridLocation baseLocation = new GridLocation())
        where T : ITreeItem<T>
        {
            var currentPosition = baseLocation;
            var positionToItems = new Dictionary<GridLocation, T>();
            foreach (var root in roots)
            {
                positionToItems[currentPosition] = root;
                var descendants = root.GetDescendants();
                foreach (var descendant in descendants)
                {
                    var nextRow = descendant.Depth;
                    if (currentPosition.Row < nextRow)
                    {
                        currentPosition.Row = nextRow;
                    }
                    else if (currentPosition.Row == nextRow)
                    {
                        currentPosition.Column++;
                    }
                    else
                    {
                        currentPosition.Row = nextRow;
                        currentPosition.Column++;
                    }

                    positionToItems[currentPosition] = descendant;
                }

                currentPosition.Column++;
                currentPosition.Row = 0;
            }

            return positionToItems;
        }

        // TODO: GetVerticalTableLayoutとGetVerticalHierarchyTableLayoutはどっちか動いてないのかな？
        public static IDictionary<GridLocation, T> GetVerticalTableLayout<T>(
            this IEnumerable<T> roots,
            GridLocation baseLocation = new GridLocation())
        where T : ITreeItem<T>
        {
            var positionToItems = new Dictionary<GridLocation, T>();

            var currentLocation = baseLocation;
            foreach (var root in roots)
            {
                positionToItems[currentLocation] = root;
                var descendants = root.GetDescendants();
                foreach (var descendant in descendants)
                {
                    var nextColumn = descendant.Depth;
                    if (currentLocation.Column < nextColumn)
                    {
                        currentLocation.Column = nextColumn;
                    }
                    else if (currentLocation.Column == nextColumn)
                    {
                        currentLocation.Row++;
                    }
                    else
                    {
                        currentLocation.Column = nextColumn;
                        currentLocation.Row++;
                    }

                    positionToItems[currentLocation] = descendant;
                }

                currentLocation.Row++;
                currentLocation.Column = 0;
            }

            return positionToItems;
        }

        // TODO: GetVerticalTableLayoutとGetVerticalHierarchyTableLayoutはどっちか動いてないのかな？
        public static IDictionary<GridLocation, T> GetVerticalHierarchyTableLayout<T>(
            this IEnumerable<T> roots, 
            GridLocation baseLocation = new GridLocation())
         where T : ITreeItem<T>
        {
            var positionToItems = new Dictionary<GridLocation, T>();

            var currentPosition = baseLocation;
            foreach (var root in roots)
            {
                positionToItems[currentPosition] = root;
                var descendants = root.GetDescendants();
                foreach (var descendant in descendants)
                {
                    currentPosition.Column = baseLocation.Column + descendant.Depth;
                    currentPosition.Row++;
                    positionToItems[currentPosition] = descendant;
                }

                currentPosition.Row++;
                currentPosition.Column = 0;
            }

            return positionToItems;
        }

        #endregion
    }
}
