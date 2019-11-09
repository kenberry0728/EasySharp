using System;

namespace EasySharp.ProgressNotifications.Core
{
    public interface IProgressNotificationService : IDisposable
    {
        void NotifyProgress(INotification notification);
    }
}
