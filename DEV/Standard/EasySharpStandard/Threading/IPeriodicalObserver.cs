using System;
using EasySharp.Collections.Specialized;

namespace EasySharp.Threading
{
    public interface IPeriodicalObserver<T> : IDisposable
    {
        IReferenceCountableEventContainer<T> ObeservedEvent { get; }
    }
}