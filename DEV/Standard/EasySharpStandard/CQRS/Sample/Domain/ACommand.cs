using System;

namespace EasySharp.CQRS.Sample
{
    public class ACommand : IIdCommand
    {
        public ACommand(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}
