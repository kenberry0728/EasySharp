using System.Diagnostics;
using EasySharp.Logs.Check.Core;
using EasySharp.Logs.Check.Core.Models;

namespace EasySharp.Logs.Check.Implementations
{
    internal class DebugCheckLogger<TErrorCode, TLocation> : ICheckLogger<TErrorCode, TLocation>
    {
        public void Write(CheckResult<TErrorCode, TLocation> checkResult, string message)
        {
            Debug.Write($"{checkResult}\t{message}");
        }
    }
}
