using System;
using System.Collections.ObjectModel;
using EasySharp.Collections.Generic;

namespace EasySharp.Collections.Specialized
{
    public class ReferenceCountableEventContainer<TEventArg> 
        : EventContainer<TEventArg>, IReferenceCountableEventContainer<TEventArg>
    {
        private readonly ObservableCollection<EventHandler<TEventArg>> handlers 
            = new ObservableCollection<EventHandler<TEventArg>>();

        public ReferenceCountableEventContainer(
            Action<EventHandler<TEventArg>> subscribeEvent,
            Action<EventHandler<TEventArg>> unsubscribeEvent)
            :base(subscribeEvent, unsubscribeEvent)
        {
            this.NotifyCollectionChanged = new NotifyCollectionChangedEventContainer(
                handler => handlers.CollectionChanged += handler,
                handler => handlers.CollectionChanged += handler);
        }
        
        public int ReferenceCount => this.handlers.Count;

        public INotifyCollectionChangedEventContainer NotifyCollectionChanged { get; }

        public override Action Subscribe(EventHandler<TEventArg> action)
        {
            lock (this.handlers.SyncRoot())
            {
                this.handlers.Add(action);
            }

            return base.Subscribe(action);
        }

        public override void Unsubscribe(EventHandler<TEventArg> action)
        {
            lock (this.handlers.SyncRoot())
            {
                this.handlers.Remove(action);
            }

            base.Unsubscribe(action);
        }
    }

    public class ReferenceCountableEventContainer : EventContainer, IReferenceCountableEventContainer
    {
        private readonly ObservableCollection<EventHandler> handlers = new ObservableCollection<EventHandler>();

        public ReferenceCountableEventContainer(
            Action<EventHandler> subscribeEvent,
            Action<EventHandler> unsubscribeEvent)
            : base(subscribeEvent, unsubscribeEvent)
        {
            this.NotifyCollectionChanged = new NotifyCollectionChangedEventContainer(
                handler => handlers.CollectionChanged += handler,
                handler => handlers.CollectionChanged += handler);
        }

        public int ReferenceCount => this.handlers.Count;

        public INotifyCollectionChangedEventContainer NotifyCollectionChanged { get; }

        public override Action Subscribe(EventHandler action)
        {
            lock (this.handlers.SyncRoot())
            {
                this.handlers.Add(action);
            }

            return base.Subscribe(action);
        }

        public override void Unsubscribe(EventHandler action)
        {
            lock (this.handlers.SyncRoot())
            {
                this.handlers.Remove(action);
            }

            base.Unsubscribe(action);
        }
    }
}
