using System;
using EasySharp.Logs.Check.Core;
using EasySharp.Logs.Check.Core.Models;

namespace EasySharpStandardConsole.Logs.CheckLogs.Implementations
{
    internal class ConsoleCheckLogger<TErrorCode, TLocation> : ICheckLogger<TErrorCode, TLocation>
    {
        public void Write(CheckResult<TErrorCode, TLocation> result, string message)
        {
            // TODO: messageはerrorCodeとmessageを結びつけるmapperを用意する。
            // mapperのデフォルトは、enumのToString()で十分。
            Console.WriteLine(
                string.Join("\t", result, message));
        }
    }
}
