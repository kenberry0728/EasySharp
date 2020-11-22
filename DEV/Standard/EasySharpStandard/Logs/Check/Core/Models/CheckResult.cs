namespace EasySharp.Logs.CheckLogs.Core.Models
{
    public class CheckResult<TErrorCode>
    {
        public CheckResult(CheckResultCategory category, TErrorCode errorCode)
        {
            this.Category = category;
            this.ErrorCode = errorCode;
        }

        CheckResultCategory Category { get; }

        public TErrorCode ErrorCode { get; }

        public override string ToString()
        {
            return $"{Category}\t{ErrorCode}";
        }
    }
}
