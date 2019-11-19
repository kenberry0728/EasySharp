using System;

namespace EasySharp.Observer
{
    public interface IDisposableValueObserver<T> : IValueObserver<T>, IDisposable, IDisposablePattern
        where T : struct
    {
    }
}
