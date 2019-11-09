using System;

namespace EasySharp
{
    public interface IEventContainer<TEventArg>
    {
        void SubscribeEvent(EventHandler<TEventArg> action);
        void UnsubscribeEvent(EventHandler<TEventArg> action);
    }
}