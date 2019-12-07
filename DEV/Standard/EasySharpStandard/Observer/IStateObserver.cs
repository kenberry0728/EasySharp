using System;

namespace EasySharp.Observer
{
    public interface IStateObserver<TValue> : IDisposable, IDisposablePattern
    {
        event EventHandler<ValueChangedEventArg<TValue>> ValueChange;

        IEventContainer<ValueChangedEventArg<TValue>> ValueChangeEvent { get; }

        TValue CurrentValue { get; }
    }
}