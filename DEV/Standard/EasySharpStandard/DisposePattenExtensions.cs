namespace EasySharp
{
    /// <summary>
    /// https://docs.microsoft.com/ja-jp/visualstudio/code-quality/ca1063
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Extensions")]
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
