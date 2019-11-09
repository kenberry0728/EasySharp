using EasySharp.Locations.Core;
using EasySharp.Logs.CheckLogs.Core;
using EasySharp.Logs.CheckLogs.Core.Models;
using System.Diagnostics;

namespace EasySharp.Logs.CheckLogs.Implementations
{
    internal class DefaultCheckLogger : ICheckLogger
    {
        public void Write(CheckResultCategories category, CheckResult code, ILocation location, string message)
        {
            Debug.Write($"{category}\t{code}\t{location}\t{message}");
        }
    }
}
