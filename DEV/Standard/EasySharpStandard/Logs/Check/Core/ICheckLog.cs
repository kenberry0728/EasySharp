using EasySharp.Location;
using EasySharp.Log.Text;
using EasySharp.Logs.CheckLogs.Core.Models;

namespace EasySharp.Logs.CheckLogs.Core
{
    public interface ICheckLog : ITextLog
    {
        CheckResult CheckResult { get; }

        ILocation Location { get; }
    }
}
