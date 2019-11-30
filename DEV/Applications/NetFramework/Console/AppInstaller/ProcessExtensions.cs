using EasySharp;
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
                if (Try.To<Process>(() =>
                {
                    return proc.MainModule.FileName.OrdinalIgnoreCaseEquals(filePath) ? proc : null;
                },
                out var process))
                {
                    return process;
                }
            }

            return null;
        }
    }
}