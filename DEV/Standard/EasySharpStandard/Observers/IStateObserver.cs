using System;

namespace EasySharpStandard.Observers
{
    public interface IStateObserver<TStateStruct>
        where TStateStruct : struct
    {
        event EventHandler<StateChangedEventArg<TStateStruct>> StateChange;

        TStateStruct CurrentState { get; }
    }
}