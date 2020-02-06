﻿using System;

namespace EasySharp.CQRS.Sample.Domain
{
    public class BCompletedEvent : IIdEvent
    {
        public BCompletedEvent(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}
