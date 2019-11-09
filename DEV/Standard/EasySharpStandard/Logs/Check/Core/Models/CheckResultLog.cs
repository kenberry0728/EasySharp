using EasySharp.Location;
using EasySharp.Log.Text.Models;

namespace EasySharp.Logs.CheckLogs.Core.Models
{
    public class CheckResultLog : TextLog, ICheckLog
    {
        public CheckResult CheckResult { get; internal set; }

        public ILocation Location { get; internal set; }
    }
}
