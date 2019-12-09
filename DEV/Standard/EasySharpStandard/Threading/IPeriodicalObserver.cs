using System;

namespace EasySharp.Threading
{
    public interface IPeriodicalObserver<T> : IDisposable
    {
        IReferenceCountableEventContainer<T> ObeservedEvent { get; }
    }
}