namespace EasySharp.CQRS
{
    public interface ISagaStartCommand : IIdCommand
    {
        ICommandSaga Create(ICommandEventBus commandEventBus);
    }
}
