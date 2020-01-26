namespace EasySharp.CQRS
{
    public interface IEvent : IMessage
    {
        object Sender { get; }
    }
}
