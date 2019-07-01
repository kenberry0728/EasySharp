using System.Diagnostics;

namespace EasySharpStandard.Processes
{
    public static class ProcessExtensions
    {
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
            string arguments = "")
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = processPath,
                    Arguments = arguments,
                }
            };

            process.Start();
            return process;
        }
    }
}
