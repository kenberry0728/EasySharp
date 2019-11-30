using System.Collections.Generic;
using System.Linq;

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

        public string TempFolder { get; set; }

        public List<string> ExcludeRelativePathRegex { get; } = new List<string>();

        public AppInstallerArgument Clone()
        {
            var arg = new AppInstallerArgument(this.RunMode)
            {
                InstallDir = this.InstallDir,
                OriginalAppPath =  this.OriginalAppPath,
                SourceDir = this.SourceDir,
                TempFolder =  this.TempFolder
            };
            arg.ExcludeRelativePathRegex.AddRange(this.ExcludeRelativePathRegex);
            return arg;
        }
    }
}