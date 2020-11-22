using EasySharp.Location;
using EasySharp.Logs.CheckLogs.Core;
using EasySharp.Logs.CheckLogs.Core.Models;
using System.Diagnostics;

namespace EasySharp.Logs.CheckLogs.Implementations
{
    internal class DebugCheckLogger<TErrorCode, TLocation> : ICheckLogger<TErrorCode, TLocation>
    {
        public void Write(CheckResultCategory category, CheckResult<TErrorCode, TLocation> code, ILocation location, string message)
        {
            Debug.Write($"{category}\t{code}\t{location}\t{message}");
        }
    }
}
