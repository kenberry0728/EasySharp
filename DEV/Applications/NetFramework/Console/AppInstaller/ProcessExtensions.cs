using System;
using System.Diagnostics;

namespace AppInstaller
{
    public static class ProcessExtensions
    {
        public static Process GetProcessByFileName(this string filePath)
        {
            // TODO: Can be standard?
            foreach (var proc in Process.GetProcesses())
            {
                try
                {
                    if (string.Equals(proc.MainModule.FileName, filePath, StringComparison.OrdinalIgnoreCase))
                    {
                        return proc;
                    }
                }
                catch
                {
                    // ignored
                }
            }

            return null;
        }
    }
}