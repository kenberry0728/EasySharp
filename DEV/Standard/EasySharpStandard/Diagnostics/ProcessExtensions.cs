using System;
using System.Diagnostics;

namespace EasySharp.Processes
{
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

        public static string RunProcessAndGetStandardOutput(this string processPath, string arguments = "")
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = processPath,
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
    }
}
