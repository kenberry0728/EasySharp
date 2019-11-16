using System;

namespace EasySharp.Observer
{
    public interface IValueObserver<TValue>
        where TValue : struct
    {
        event EventHandler<ValueChangedEventArg<TValue>> ValueChange;

        IEventContainer<ValueChangedEventArg<TValue>> ValueChangeEvent { get; }

        TValue CurrentValue { get; }
    }
}