using System;

namespace EasySharp.CQRS.Sample.Application
{
    public class AandBCompletedEvent : IIdEvent
    {
        public AandBCompletedEvent(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}
