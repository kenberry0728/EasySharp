using System;
using EasySharpStandard.ProgressNotifications.Core;

namespace EasySharpStandard.ProgreeNotifications.Core
{
    public interface IProgressNotificationService : IDisposable
    {
        void NotifyProgress(INotification notification);
    }
}
