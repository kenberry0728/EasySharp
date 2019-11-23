using System;

namespace EasySharp.Threading
{
    public interface IPeriodicalObserver<T> : IDisposable
    {
        IEventContainer<T> ObeservedEvent { get; }

        event EventHandler<T> Observed;

        void DisposeManagedResources();
    }
}