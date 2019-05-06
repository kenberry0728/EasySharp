using System;
using System.Collections.Generic;
using System.Text;

namespace EasySharpStandard.ProgreeNotifications.Core
{
    public interface IProgressNotificationService : IDisposable
    {
        void NotifyProgress(INotification notification);
    }
}
