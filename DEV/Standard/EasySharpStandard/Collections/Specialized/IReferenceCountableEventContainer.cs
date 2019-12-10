
using EasySharp.Collections.Specialized;
using System.Collections.Specialized;

namespace EasySharp
{
    public interface IReferenceCountableEventContainer<TEventArg> 
        : IEventContainer<TEventArg>, INotifyCollectionChanged
    {
        int ReferenceCount { get; }

        INotifyCollectionChangedEventContainer NotifyCollectionChanged { get; }
    }

    public interface IReferenceCountableEventContainer 
        : IEventContainer, INotifyCollectionChanged
    {
        int ReferenceCount { get; }

        INotifyCollectionChangedEventContainer NotifyCollectionChanged { get; }
    }
}