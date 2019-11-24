using System;

namespace EasySharp
{
    public interface IEventContainer<TEventArg>
    {
        void Subscribe(EventHandler<TEventArg> action);
        void Unsubscribe(EventHandler<TEventArg> action);
    }
}