using System;
using System.Collections.Specialized;

namespace EasySharp.Collections.Specialized
{
    public interface INotifyCollectionChangedEventContainer
    {
        Action Subscribe(NotifyCollectionChangedEventHandler action);
        void Unsubscribe(NotifyCollectionChangedEventHandler action);
    }
}