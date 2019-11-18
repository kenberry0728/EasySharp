using System;

namespace EasySharp
{
    /// <summary>
    /// https://docs.microsoft.com/ja-jp/visualstudio/code-quality/ca1063
    /// </summary>
    public abstract class DisposableBase : IDisposable, IDisposablePattern
    {
        ~DisposableBase()
        {
            this.OnDestruct();
        }

        public void Dispose()
        {
            this.OnDispose();
        }

        public abstract void DisposeNativeResources();

        public abstract void DisposeManagedResources();
    }
}
