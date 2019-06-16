namespace AppInstaller.Core.Results
{
    public class Result
    {
        public ResultCode ResultCode { get; set; }

        public bool Updated { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}