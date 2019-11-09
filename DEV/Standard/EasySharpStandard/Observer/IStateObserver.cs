using System;

namespace EasySharp.Observer
{
    public interface IStateObserver<TStateStruct>
        where TStateStruct : struct
    {
        event EventHandler<StateChangedEventArg<TStateStruct>> StateChange;

        TStateStruct CurrentState { get; }
    }
}