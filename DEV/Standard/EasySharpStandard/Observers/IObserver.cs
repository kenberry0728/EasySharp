using System;

namespace EasySharpStandard.Observers
{
    public interface IObserver<TState>
    {
        event EventHandler<StateChangedEventArg<TState>> StateChange;

        TState CurrentState { get; }
    }
}