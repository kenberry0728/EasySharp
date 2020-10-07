using EasySharpStandardConsoleTest.ProgressNotifications;
using EasySharpStandardTestCore.Core;
using System;

namespace EasySharpStandardConsoleTest
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            new Test(new ConsoleProgressNotificationServiceTests()).Run();

            new Test(new EventContainerTests()).Run();
        }
    }
}
