using System;
using System.Collections.Generic;

namespace EasySharp
{
    public class ReferenceCountableEventContainer<TEventArg> : EventContainer<TEventArg>, IReferenceCountableEventContainer<TEventArg>
    {
        private readonly List<EventHandler<TEventArg>> handlers = new List<EventHandler<TEventArg>>();

        public ReferenceCountableEventContainer(
            Action<EventHandler<TEventArg>> subscribeEvent,
            Action<EventHandler<TEventArg>> unsubscribeEvent)
            :base(subscribeEvent, unsubscribeEvent)
        {
        }

        public int ReferenceCount => this.handlers.Count;

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
        private readonly List<EventHandler> handlers = new List<EventHandler>();

        public ReferenceCountableEventContainer(
            Action<EventHandler> subscribeEvent,
            Action<EventHandler> unsubscribeEvent)
            : base(subscribeEvent, unsubscribeEvent)
        {
        }

        public int ReferenceCount => this.handlers.Count;

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
