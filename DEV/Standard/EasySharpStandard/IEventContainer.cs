using System;

namespace EasySharp
{
    public interface IEventContainer<TEventArg>
    {
        Action Subscribe(EventHandler<TEventArg> action);

        Action<EventHandler<TEventArg>> Unsubscribe { get; }
    }
}