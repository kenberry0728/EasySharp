using EasySharp.Locations.Core;
using EasySharp.Logs.TextLogs.Core.Models;

namespace EasySharp.Logs.CheckLogs.Core.Models
{
    public class CheckResultLog : TextLog, ICheckLog
    {
        public CheckResult CheckResult { get; internal set; }

        public ILocation Location { get; internal set; }
    }
}
