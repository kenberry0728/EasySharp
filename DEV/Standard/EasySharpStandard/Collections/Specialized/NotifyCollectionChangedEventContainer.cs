using System;
using System.Collections.Specialized;

namespace EasySharp.Collections.Specialized
{
    public class NotifyCollectionChangedEventContainer : INotifyCollectionChangedEventContainer
    {
        private readonly Action<NotifyCollectionChangedEventHandler> subscribeEvent;
        private readonly Action<NotifyCollectionChangedEventHandler> unsubscribeEvent;

        public NotifyCollectionChangedEventContainer(
            Action<NotifyCollectionChangedEventHandler> subscribeEvent,
            Action<NotifyCollectionChangedEventHandler> unsubscribeEvent)
        {
            this.subscribeEvent = subscribeEvent;
            this.unsubscribeEvent = unsubscribeEvent;
        }

        public virtual void Unsubscribe(NotifyCollectionChangedEventHandler action)
        {
            this.unsubscribeEvent(action);
        }

        public virtual Action Subscribe(NotifyCollectionChangedEventHandler action)
        {
            this.subscribeEvent(action);
            return () => this.Unsubscribe(action);
        }
    }
}
