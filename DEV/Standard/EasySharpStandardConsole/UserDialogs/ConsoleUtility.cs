using System;

namespace EasySharpStandardConsole.UserDialogs
{
    public static class ConsoleUtility
    {
        public static string QestionAndAnswer(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine(); ;
        }
    }
}
