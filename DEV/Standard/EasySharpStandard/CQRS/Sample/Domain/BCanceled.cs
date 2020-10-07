using System;

namespace EasySharp.CQRS.Sample.Domain
{
    public class BCanceledEvent : IIdEvent
    {
        public BCanceledEvent(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}
