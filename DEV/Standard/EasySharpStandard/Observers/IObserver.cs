using System;

namespace EasySharpStandard.Observers
{
    public interface IObserver<TStateEnum> where TStateEnum : struct
    {
        event EventHandler<StateChangedEventArg<TStateEnum>> StateChange;
    }
}