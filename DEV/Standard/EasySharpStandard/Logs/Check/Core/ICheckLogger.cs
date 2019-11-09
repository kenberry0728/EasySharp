using EasySharp.Location;
using EasySharp.Logs.CheckLogs.Core.Models;

namespace EasySharp.Logs.CheckLogs.Core
{
    public interface ICheckLogger
    {
        void Write(CheckResultCategories category, CheckResult code, ILocation location, string message);
    }
}
