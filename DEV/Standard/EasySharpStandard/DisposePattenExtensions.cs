using System;

namespace EasySharp
{
    /// <summary>
    /// https://docs.microsoft.com/ja-jp/visualstudio/code-quality/ca1063
    /// </summary>
    public static class DisposePattenExtensions
    {
        public static void OnDispose(this IDisposablePattern disposablePattern)
        {
            foreach (var disposeAction in disposablePattern.DisposeActions)
            {
                disposeAction();
            }

            disposablePattern.DisposeActions.Clear();

            disposablePattern.DisposeNativeResources();
            GC.SuppressFinalize(disposablePattern);
        }

        // NOTE: Leave out the finalizer altogether if this class doesn't
        // own unmanaged resources, but leave the other methods
        // exactly as they are.
        public static void OnDestruct(this IDisposablePattern disposablePattern)
        {
            // Finalizer calls Dispose(false)
            disposablePattern.DisposeNativeResources();
        }
    }
}
