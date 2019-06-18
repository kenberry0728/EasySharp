using System.Collections.Generic;

namespace AppInstaller.Core.Arguments
{
    public class AppInstallerArgument
    {
        public AppInstallerArgument()
        {
        }

        public AppInstallerArgument(RunMode runMode)
        {
            RunMode = runMode;
        }

        public RunMode RunMode { get; set; }

        public string SourceDir { get; set; } = string.Empty;

        public string InstallDir { get; set; } = string.Empty;

        public string OriginalAppPath { get; set; } = string.Empty;

        public string TempFolder { get; set; } = null;

        public List<string> ExcludePathRegex { get; set; } = new List<string>();
    }
}