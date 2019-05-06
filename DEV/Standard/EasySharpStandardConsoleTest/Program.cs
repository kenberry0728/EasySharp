using EashSharpStandardTestCore.Core;
using EasySharpStandard.ProgreeNotifications.Core;
using EasySharpStandard.ProgreeNotifications.Core.Models;
using EasySharpStandardConsole.ProgressNotifications.Core;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

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
