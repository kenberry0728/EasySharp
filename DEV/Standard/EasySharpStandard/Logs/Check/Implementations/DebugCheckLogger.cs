using EasySharp.Logs.CheckLogs.Core;
using EasySharp.Logs.CheckLogs.Core.Models;
using System.Diagnostics;

namespace EasySharp.Logs.CheckLogs.Implementations
{
    internal class DebugCheckLogger<TErrorCode, TLocation> : ICheckLogger<TErrorCode, TLocation>
    {
        public void Write(CheckResult<TErrorCode, TLocation> checkResult, string message)
        {
            Debug.Write($"{checkResult}\t{message}");
        }
    }
}
