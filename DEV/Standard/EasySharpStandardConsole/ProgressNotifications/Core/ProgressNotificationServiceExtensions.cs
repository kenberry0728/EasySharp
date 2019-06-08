using EasySharpStandard.ProgressNotifications.Core;
using EasySharpStandardConsole.ProgressNotifications.Implementations;

namespace EasySharpStandardConsole.ProgressNotifications.Core
{
    public static class ProgressNotificationServiceExtensions
    {
        public static IProgressNotificationService Resolve(IProgressNotificationService service)
        {
            return service ?? new ConsoleProgressNotificationService();
        }
    }
}
