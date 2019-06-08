using System;
using EasySharpStandard.ProgressNotifications.Core;

namespace EasySharpStandardConsole.ProgressNotifications.Implementations
{
    internal class ConsoleProgressNotificationService : IProgressNotificationService
    {
        internal ConsoleProgressNotificationService()
        {
        }

        public void Dispose()
        {
            Console.WriteLine();
        }

        public void NotifyProgress(INotification notification)
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(notification.Message);
        }
    }
}
