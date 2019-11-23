using System;
using System.Linq;
using System.Reflection;

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

        public static void DisposeMembers(this IDisposable disposable)
        {
            var type = disposable.GetType();
            var properties = type.GetProperties(
                BindingFlags.Instance
                | BindingFlags.Public
                | BindingFlags.NonPublic);

            foreach (var property in properties.Where(p => p.CanRead))
            {
                var disposableProperty = property.GetValue(disposable) as IDisposable;
                if (disposableProperty != null)
                {
                    disposableProperty.Dispose();
                }
            }

            var fields = type.GetFields(
                BindingFlags.Instance
                | BindingFlags.Public
                | BindingFlags.NonPublic);
            foreach (var field in fields)
            {
                var disposableField = field.GetValue(disposable) as IDisposable;
                disposableField.Dispose();
            }
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
