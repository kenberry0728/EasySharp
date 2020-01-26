using System;

namespace EasySharp.CQRS
{
    public interface ICommandSagaFactory
    {
        Type CommandType { get; }

        ICommandSaga Create(Guid id);
    }
}
