namespace EasySharpStandard.ProgreeNotifications.Core.Models
{
    public class PercentageNotification : Notification, IPercentageNotification
    {
        public double Percentage { get; set; }
    }
}
