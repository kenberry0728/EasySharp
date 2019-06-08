namespace EasySharpStandard.ProgreeNotifications.Core
{
    public interface IPercentageProgressNotificationService
    {
        void NotifyProgree(IPercentageNotification percentageNotification);
    }
}
