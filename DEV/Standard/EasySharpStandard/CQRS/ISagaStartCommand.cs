using System;

namespace EasySharp.CQRS
{
    public interface ISagaStartCommand : IIdCommand
    {
        ICommandSaga Create(Guid id);
    }
}
