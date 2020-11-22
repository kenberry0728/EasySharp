using EasySharp.Location;
using EasySharp.Logs.CheckLogs.Core.Models;

namespace EasySharp.Logs.CheckLogs.Core
{
    public interface ICheckLogger<TErrorCode>
    {
        void Write(CheckResultCategory category, CheckResult<TErrorCode> code, ILocation location, string message);
    }
}
