using System;

namespace EasySharp.CQRS.Sample
{
    public class BCommand : IIdCommand
    {
        public BCommand(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}
