namespace EasySharp.CQRS
{
    public interface IIdEventListner : IIdEventType
    {
        bool CanHandle(IIdEventType idEvent);
    }
}
