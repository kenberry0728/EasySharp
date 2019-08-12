using EasySharpStandard.Locations.Core;
using EasySharpStandard.Logs.CheckLogs.Core;
using EasySharpStandard.Logs.CheckLogs.Core.Models;
using System.Diagnostics;

namespace EasySharpStandard.Logs.CheckLogs.Implementations
{
    internal class DefaultCheckLogger : ICheckLogger
    {
        public void Write(CheckResultCategories category, CheckResult code, ILocation location, string message)
        {
            Debug.Write($"{category}\t{code}\t{location}\t{message}");
        }
    }
}
