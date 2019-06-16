namespace AppInstaller.Core.Arguments
{
    public class AppInstallerArgument
    {
        public RunMode RunMode { get; set; }

        public string SourceDir { get; set; } = string.Empty;

        public string InstallDir { get; set; } = string.Empty;

        public string RebootAppPath { get; set; } = string.Empty;
    }
}