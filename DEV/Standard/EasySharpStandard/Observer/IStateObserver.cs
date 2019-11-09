using System;

namespace EasySharp.Observer
{
    public interface IStateObserver<TStateStruct>
        where TStateStruct : struct
    {
        event EventHandler<StateChangedEventArg<TStateStruct>> StateChange;

        IEventContainer<StateChangedEventArg<TStateStruct>> StateChangeEvent { get; }

        TStateStruct CurrentState { get; }
    }
}