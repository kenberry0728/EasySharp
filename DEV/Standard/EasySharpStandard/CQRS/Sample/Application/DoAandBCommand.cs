using System;

namespace EasySharp.CQRS.Sample.Application
{
    public class DoAandBCommand : IIdCommand
    {
        public Guid Id => Guid.NewGuid();
    }
}
