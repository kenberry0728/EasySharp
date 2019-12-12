using System;
using System.Collections.Generic;

namespace EasySharp
{
    public interface IDisposablePattern : IDisposable
    {
        IList<Action> DisposeActions { get; }
        void DisposeNativeResources();
    }
}
