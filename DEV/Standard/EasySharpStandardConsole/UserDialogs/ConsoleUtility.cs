using System;
using System.Collections.Generic;
using System.Text;

namespace EasySharpStandardConsole.UserDialogs
{
    public static class ConsoleUtility
    {
        public static string QandA(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine(); ;
        }
    }
}
