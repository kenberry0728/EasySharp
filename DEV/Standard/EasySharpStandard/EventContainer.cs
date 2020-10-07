using System;

namespace EasySharp
{
    public class EventContainer<TEventArg> : IEventContainer<TEventArg>
    {
        private readonly Action<EventHandler<TEventArg>> subscribeEvent;
        private readonly Action<EventHandler<TEventArg>> unsubscribeEvent;

        public EventContainer(
            Action<EventHandler<TEventArg>> subscribeEvent,
            Action<EventHandler<TEventArg>> unsubscribeEvent)
        {
            this.subscribeEvent = subscribeEvent;
            this.unsubscribeEvent = unsubscribeEvent;
        }

        public virtual void Unsubscribe(EventHandler<TEventArg> action)
        {
            this.unsubscribeEvent(action);
        }

        public virtual Action Subscribe(EventHandler<TEventArg> action)
        {
            this.subscribeEvent(action);
            return () => this.Unsubscribe(action);
        }
    }

    public class EventContainer : IEventContainer
    {
        private readonly Action<EventHandler> subscribeEvent;
        private readonly Action<EventHandler> unsubscribeEvent;

        public EventContainer(
            Action<EventHandler> subscribeEvent,
            Action<EventHandler> unsubscribeEvent)
        {
            this.subscribeEvent = subscribeEvent;
            this.unsubscribeEvent = unsubscribeEvent;
        }

        public virtual void Unsubscribe(EventHandler action)
        {
            this.unsubscribeEvent(action);
        }

        public virtual Action Subscribe(EventHandler action)
        {
            this.subscribeEvent(action);
            return () => this.Unsubscribe(action);
        }
    }
}
