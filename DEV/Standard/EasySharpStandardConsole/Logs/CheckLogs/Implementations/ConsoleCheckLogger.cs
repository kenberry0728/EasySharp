using EasySharp.Logs.CheckLogs.Core;
using EasySharp.Logs.CheckLogs.Core.Models;
using System;

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
