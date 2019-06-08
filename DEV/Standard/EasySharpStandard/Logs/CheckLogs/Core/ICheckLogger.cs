using EasySharpStandard.Locations.Core;
using EasySharpStandard.Logs.CheckLogs.Core.Models;

namespace EasySharpStandard.Logs.CheckLogs.Core
{
    public interface ICheckLogger
    {
        void Write(CheckResultCategories category, CheckResult code, ILocation location, string message);
    }
}
