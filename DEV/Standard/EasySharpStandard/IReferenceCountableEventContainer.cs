
namespace EasySharp
{
    public interface IReferenceCountableEventContainer<TEventArg> : IEventContainer<TEventArg>
    {
        int ReferenceCount { get; }
    }

    public interface IReferenceCountableEventContainer : IEventContainer
    {
        int ReferenceCount { get; }
    }
}