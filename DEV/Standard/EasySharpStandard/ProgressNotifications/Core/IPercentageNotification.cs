namespace EasySharp.ProgressNotifications.Core
{
    public interface IPercentageNotification : INotification
    {
        double Percentage { get; }
    }
}
