using System.Diagnostics;

namespace EasySharpStandard.Processes
{
    public static class ProcessExtensions
    {
        public static string RunProcessAndGetStandardOutput(this string processPath, string arguments = "")
        {
            Debug.Assert(arguments == "", "TODO : check if it works or not.");
            var process = new Process
            {
                StartInfo = new ProcessStartInfo()
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
    }
}
