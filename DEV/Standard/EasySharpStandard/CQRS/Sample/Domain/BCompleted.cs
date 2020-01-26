using System;

namespace EasySharp.CQRS.Sample.Domain
{
    public class BCompletedEvent : IEvent
    {
        public BCompletedEvent(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}
