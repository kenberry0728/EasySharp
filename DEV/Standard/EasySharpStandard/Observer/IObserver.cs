using System;

namespace EasySharp.Observer
{
    public interface IObserver<TStateStruct>
        where TStateStruct : struct
    {
        event EventHandler<ValueChangedEventArg<TStateStruct>> StateChange;

        IEventContainer<ValueChangedEventArg<TStateStruct>> StateChangeEvent { get; }

        TStateStruct CurrentState { get; }
    }
}