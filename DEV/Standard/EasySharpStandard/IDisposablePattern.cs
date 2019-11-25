using System;
using System.Collections.Generic;

namespace EasySharp
{
    public interface IDisposablePattern
    {
        IList<Action> DisposeActions { get; }
        void DisposeNativeResources();
    }
}
