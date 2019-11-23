using System;

namespace EasySharp.Threading
{
    public interface IPeriodicalObserver<T>
    {
        IEventContainer<T> ObeservedEvent { get; }

        event EventHandler<T> Observed;

        void DisposeManagedResources();
    }
}