using EasySharpStandard.Locations.Core;
using EasySharpStandard.Logs.TextLogs.Core.Models;

namespace EasySharpStandard.Logs.CheckLogs.Core.Models
{
    public class CheckResultLog : TextLog, ICheckLog
    {
        public CheckResutl CheckResult { get; internal set; }

        public ILocation Location { get; internal set; }
    }
}
