using System;
using EasySharpStandardTestCore.Core;
using EasySharpStandardConsoleTest.ProgressNotifications;

namespace EasySharpStandardConsoleTest
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            new Test(new ConsoleProgressNotificationServiceTests()).Run();
        }
    }
}
