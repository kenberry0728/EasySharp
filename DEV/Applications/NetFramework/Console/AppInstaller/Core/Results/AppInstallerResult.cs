using AppInstaller.Implementation;

namespace AppInstaller.Core.Results
{
    public class AppInstallerResult
    {
        public ResultCode ResultCode { get; set; }

        public bool Updated { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}