using System;

namespace EasySharp.Threading
{
    public interface IPeriodicalObserver<T> : IDisposable
    {
        IEventContainer<T> ObeservedEvent { get; }
    }
}