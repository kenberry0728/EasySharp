using EasySharpStandard.ProgressNotifications.Core;

namespace EasySharpStandard.ProgreeNotifications.Core
{
    public interface IPercentageNotification : INotification
    {
        double Percentage { get; }
    }
}
