using System.Collections.Generic;

namespace EasySharp.Logs.CheckLogs.Core.Models
{
    public class CheckResult<TErrorCode>
    {
        public CheckResult(
            CheckResultCategory category,
            TErrorCode errorCode,
            params string[] detailInfo)
        {
            this.Category = category;
            this.ErrorCode = errorCode;
            this.DetailInfo = detailInfo;
        }

        CheckResultCategory Category { get; }

        public TErrorCode ErrorCode { get; }
        
        public IEnumerable<string> DetailInfo { get; }

        public override string ToString()
        {
            return $"{Category}\t{ErrorCode}";
        }
    }
}
