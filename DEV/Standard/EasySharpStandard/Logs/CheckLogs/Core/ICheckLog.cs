using EasySharp.Location;
using EasySharp.Logs.CheckLogs.Core.Models;
using EasySharp.Logs.TextLogs.Core;

namespace EasySharp.Logs.CheckLogs.Core
{
    public interface ICheckLog : ITextLog
    {
        CheckResult CheckResult { get; }

        ILocation Location { get; }
    }
}
