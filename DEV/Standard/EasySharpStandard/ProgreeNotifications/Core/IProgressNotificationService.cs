using System;

namespace EasySharpStandard.ProgreeNotifications.Core
{
    public interface IProgressNotificationService : IDisposable
    {
        void NotifyProgress(INotification notification);
    }
}
