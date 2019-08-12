using EasySharpStandard.Locations.Core;
using EasySharpStandard.Logs.CheckLogs.Core.Models;
using EasySharpStandard.Logs.TextLogs.Core;

namespace EasySharpStandard.Logs.CheckLogs.Core
{
    public interface ICheckLog : ITextLog
    {
        CheckResult CheckResult { get; }

        ILocation Location { get; }
    }
}
