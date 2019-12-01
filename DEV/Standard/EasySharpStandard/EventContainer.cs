using System;

namespace EasySharp
{
    public class EventContainer<TEventArg> : IEventContainer<TEventArg>
    {
        private readonly Action<EventHandler<TEventArg>> subscribeEvent;

        public EventContainer(
            Action<EventHandler<TEventArg>> subscribeEvent,
            Action<EventHandler<TEventArg>> unsubscribeEvent)
        {
            this.subscribeEvent = subscribeEvent;
            this.Unsubscribe = unsubscribeEvent;
        }

        public Action<EventHandler<TEventArg>> Unsubscribe { get; }

        public Action Subscribe(EventHandler<TEventArg> action)
        {
            this.subscribeEvent(action);
            return () => this.Unsubscribe(action);
        }
    }
}
