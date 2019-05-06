using System;
using System.Collections.Generic;
using System.Text;

namespace EasySharpStandard.ProgreeNotifications.Core
{
    public interface IPercentageProgressNotificationService
    {
        void NotifyProgree(IPercentageNotification percentageNotification);
    }
}
