using System;

namespace EasySharp.CQRS.Sample.Domain
{
    public class ACanceledEvent : IIdEvent
    {
        public ACanceledEvent(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}
