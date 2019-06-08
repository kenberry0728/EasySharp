using System;
using EasySharpStandard.ProgreeNotifications.Core;
using EasySharpStandard.ProgreeNotifications.Core.Models;
using EasySharpStandardConsole.ProgressNotifications.Core;
using EasySharpStandardConsoleTest.ConsoleTests.Implementations;

namespace EasySharpStandardConsoleTest.ProgressNotifications
{
    internal class ConsoleProgressNotificationServiceTests : ConsoleTestBase
    {
        private static IProgressNotificationService CreateProgressNotificationService()
        {
            return ProgressNotificationServiceExtensions.Resolve(null);
        }

        public override void Run()
        {
            var notificationService = CreateProgressNotificationService();
            {
                notificationService.NotifyProgress(new Notification("message1"));
                notificationService.NotifyProgress(new Notification("message2"));
                notificationService.NotifyProgress(new Notification("message3"));
                notificationService.NotifyProgress(new Notification("message4"));
            }

            Console.WriteLine("after");
        }
    }
}
