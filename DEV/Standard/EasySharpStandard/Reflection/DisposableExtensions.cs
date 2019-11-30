using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace EasySharp.Reflection
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Extensions")]
    public static class DisposableExtensions
    {
        public static void DisposeMembers(this IDisposable disposable)
        {
            Debug.Assert(false, "親がDisposeだからといって子供を全部Disposeするのはやりすぎ。再考。");
            var type = disposable.GetType();
            var properties = type.GetProperties(
                BindingFlags.Instance
                | BindingFlags.Public
                | BindingFlags.NonPublic);

            var disposedObjects = new List<IDisposable>();
            foreach (var property in properties.Where(p => p.CanRead))
            {
                var disposableProperty = property.GetValue(disposable) as IDisposable;
                if (disposableProperty != null)
                {
                    disposableProperty.Dispose();
                    disposedObjects.Add(disposableProperty);
                }
            }

            var fields = type.GetFields(
                BindingFlags.Instance
                | BindingFlags.Public
                | BindingFlags.NonPublic);
            foreach (var field in fields)
            {
                var disposableField = field.GetValue(disposable) as IDisposable;
                if (disposableField != null && !disposedObjects.Contains(disposableField))
                {
                    disposableField.Dispose();
                }
            }
        }
    }
}
