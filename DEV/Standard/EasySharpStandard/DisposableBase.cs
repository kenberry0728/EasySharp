using System;
using System.Collections.Generic;

namespace EasySharp
{
    /// <summary>
    /// https://docs.microsoft.com/ja-jp/visualstudio/code-quality/ca1063
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "Considered")]
    public abstract class DisposableBase : IDisposablePattern
    {
        public IList<Action> DisposeActions { get; } = new List<Action>();

        ~DisposableBase()
        {
            this.OnDestruct();
        }

        public void Dispose()
        {
            this.OnDispose();
            GC.SuppressFinalize(this);
        }

        public virtual void DisposeNativeResources()
        {
        }
    }
}
