﻿using EasySharp.Location;
using EasySharp.Logs.CheckLogs.Core;
using EasySharp.Logs.CheckLogs.Core.Models;
using System.Diagnostics;

namespace EasySharp.Logs.CheckLogs.Implementations
{
    internal class DefaultCheckLogger<TErrorCode> : ICheckLogger<TErrorCode>
    {
        public void Write(CheckResultCategory category, CheckResult<TErrorCode> code, ILocation location, string message)
        {
            Debug.Write($"{category}\t{code}\t{location}\t{message}");
        }
    }
}
