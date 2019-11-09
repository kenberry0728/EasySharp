namespace EasySharp.ProgressNotifications.Core
{
    public interface IPercentageProgressNotificationService
    {
        void NotifyProgress(IPercentageNotification percentageNotification);
    }
}
