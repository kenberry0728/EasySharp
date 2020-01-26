namespace EasySharp.CQRS
{
    public interface ICommandSaga : IIdCommand, IIdEventListner, IEventListner
    {
        bool IsComplete { get; }
    }
}
