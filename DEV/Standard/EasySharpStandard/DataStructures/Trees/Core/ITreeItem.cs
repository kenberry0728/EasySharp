﻿using System.Collections.Generic;

namespace EasySharp.DataStructures.Trees.Core
{
    public interface ITreeItem<T>
    {
        T GetParent();
        IEnumerable<T> GetChildren();

        int Depth { get; }
        bool IsRoot { get; }
        bool IsLeaf { get; }
    }
}