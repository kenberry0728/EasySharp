using EashSharpStandardTestCore.Core;
using System;

namespace EasySharpConsoleTest
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
