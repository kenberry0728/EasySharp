using System;
using System.Collections.Generic;

namespace EasySharp
{
    /// <summary>
    /// https://docs.microsoft.com/ja-jp/visualstudio/code-quality/ca1063
    /// </summary>
    public abstract class DisposableBase : IDisposable, IDisposablePattern
    {
        public IList<Action> DisposeActions { get; } = new List<Action>();

        ~DisposableBase()
        {
            this.OnDestruct();
        }

        public void Dispose()
        {
            this.OnDispose();
        }

        public virtual void DisposeNativeResources()
        {
        }
    }
}
