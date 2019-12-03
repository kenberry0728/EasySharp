using EasySharp.IO;
using System;
using System.Diagnostics;

namespace EasySharp.Processes
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Extensions")]
    public static class ProcessExtensions
    {
        public static void HandleExitedEvent(this Process process, EventHandler eventHandler)
        {
            if (!process.EnableRaisingEvents && !process.HasExited)
            {
                process.EnableRaisingEvents = true;
            }

            process.Exited += eventHandler;
        }

        public static string RunProcessAndGetStandardOutput(this FilePath processFilePath, string arguments = "")
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = processFilePath.Value,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = false,
                    CreateNoWindow = true,
                    Arguments = arguments,
                }
            };

            process.Start();
            var result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            process.Close();
            return result;
        }

        public static Process RunProcess(
            this string processPath,
            string arguments = "",
            bool createNoWindow = false)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = processPath,
                    Arguments = arguments,
                    CreateNoWindow = createNoWindow
                }
            };

            process.Start();
            return process;
        }

        public static Process GetProcessByFileName(this IFilePath filePath)
        {
            foreach (var proc in Process.GetProcesses())
            {
                if (Try.To(() =>
                {
                    return proc.MainModule.FileName.ToFilePath().Equals(filePath) ? proc : null;
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
