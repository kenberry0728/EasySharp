namespace EasySharp.CQRS
{
    public interface IEventListner : IEvent
    {
        bool CanHandle(IEvent idEvent);
    }
}
