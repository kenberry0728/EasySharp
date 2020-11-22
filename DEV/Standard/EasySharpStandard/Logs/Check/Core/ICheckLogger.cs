using EasySharp.Location;
using EasySharp.Logs.CheckLogs.Core.Models;

namespace EasySharp.Logs.CheckLogs.Core
{
    public interface ICheckLogger<TErrorCode, TLocation>
    {
        void Write(CheckResultCategory category, CheckResult<TErrorCode, TLocation> result, ILocation location, string message);
    }
}
