namespace AppInstaller.Core.Arguments
{
    public class Argument
    {
        public RunMode RunMode { get; set; }

        public string SourceDir { get; set; } = string.Empty;

        public string InstallDir { get; set; } = string.Empty;
    }
}