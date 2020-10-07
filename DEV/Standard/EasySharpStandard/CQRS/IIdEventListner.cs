namespace EasySharp.CQRS
{
    public interface IIdEventListner : IIdEvent
    {
        bool CanHandle(IIdEvent idEvent);
    }
}
