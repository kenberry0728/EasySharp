using EasySharp.CQRS.Sampel;
using EasySharp.CQRS.Sample.Application;
using System;

namespace EasySharp.CQRS.Sample
{
    public class DoAandBSagaFactory : ICommandSagaFactory
    {
        public Type CommandType => typeof(DoAandBCommand);

        public ICommandSaga Create()
        {
            return new AandBSaga();
        }
    }
}
