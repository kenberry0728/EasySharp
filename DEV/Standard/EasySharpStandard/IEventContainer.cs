using System;

namespace EasySharp
{
    public interface IEventContainer<TEventArg>
    {
        Action Subscribe(EventHandler<TEventArg> action);
        void Unsubscribe(EventHandler<TEventArg> action);    
    }

    public interface IEventContainer
    {
        Action Subscribe(EventHandler action);

        void Unsubscribe(EventHandler action);
    }
}