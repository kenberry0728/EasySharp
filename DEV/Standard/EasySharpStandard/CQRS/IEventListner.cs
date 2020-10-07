namespace EasySharp.CQRS
{
    public interface IEventListner
    {
        bool CanHandle(IEvent idEvent);
    }
}
