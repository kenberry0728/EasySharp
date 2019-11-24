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
            disposablePattern.Dispose(true);
            GC.SuppressFinalize(disposablePattern);
        }

        // NOTE: Leave out the finalizer altogether if this class doesn't
        // own unmanaged resources, but leave the other methods
        // exactly as they are.
        public static void OnDestruct(this IDisposablePattern disposablePattern)
        {
            // Finalizer calls Dispose(false)
            disposablePattern.Dispose(false);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        private static void Dispose(this IDisposablePattern disposablePattern, bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                disposablePattern.DisposeManagedResources();
            }

            // free native resources if there are any.
            disposablePattern.DisposeNativeResources();
        }
    }
}
