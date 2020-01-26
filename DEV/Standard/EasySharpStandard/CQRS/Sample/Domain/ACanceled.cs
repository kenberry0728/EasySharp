using System;

namespace EasySharp.CQRS.Sample.Domain
{
    public class ACanceledEvent : IEvent
    {
        public ACanceledEvent(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}
