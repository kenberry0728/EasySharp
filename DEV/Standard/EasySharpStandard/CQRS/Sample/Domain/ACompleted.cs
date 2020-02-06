using System;

namespace EasySharp.CQRS.Sample.Domain
{
    public class ACompletedEvent : IIdEvent
    {
        public ACompletedEvent(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}
