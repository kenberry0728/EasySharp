using System;

namespace EasySharp
{
    public class EventContainer<TEventArg> : IEventContainer<TEventArg>
    {
        private readonly Action<EventHandler<TEventArg>> subscribeEvent;
        private readonly Action<EventHandler<TEventArg>> unsubscrieEvent;

        public EventContainer(
            Action<EventHandler<TEventArg>> subscribeEvent,
            Action<EventHandler<TEventArg>> unsubscrieEvent)
        {
            this.subscribeEvent = subscribeEvent;
            this.unsubscrieEvent = unsubscrieEvent;
        }

        public void SubscribeEvent(EventHandler<TEventArg> action)
        {
            this.subscribeEvent(action);
        }

        public void UnsubscribeEvent(EventHandler<TEventArg> action)
        {
            this.unsubscrieEvent(action);
        }
    }
}
