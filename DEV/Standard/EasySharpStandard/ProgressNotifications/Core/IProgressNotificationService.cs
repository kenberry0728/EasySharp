using System;

namespace EasySharpStandard.ProgressNotifications.Core
{
    public interface IProgressNotificationService : IDisposable
    {
        void NotifyProgress(INotification notification);
    }
}
