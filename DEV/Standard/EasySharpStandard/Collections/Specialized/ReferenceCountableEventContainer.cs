using EasySharp.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace EasySharp
{
    public class ReferenceCountableEventContainer<TEventArg> 
        : EventContainer<TEventArg>, IReferenceCountableEventContainer<TEventArg>
    {
        private readonly ObservableCollection<EventHandler<TEventArg>> handlers = new ObservableCollection<EventHandler<TEventArg>>();

        public event NotifyCollectionChangedEventHandler CollectionChanged;

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
            this.handlers.Add(action);
            return base.Subscribe(action);
        }

        public override void Unsubscribe(EventHandler<TEventArg> action)
        {
            this.handlers.Remove(action);
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

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public override Action Subscribe(EventHandler action)
        {
            this.handlers.Add(action);
            return base.Subscribe(action);
        }

        public override void Unsubscribe(EventHandler action)
        {
            this.handlers.Remove(action);
            base.Unsubscribe(action);
        }
    }
}
