using System.Collections.Generic;

namespace EasySharp.Logs.Check.Core.Models
{
    public class CheckResult<TErrorCode, TLocation>
    {
        public CheckResult(
            CheckResultCategory category,
            TErrorCode errorCode,
            TLocation  location,
            params string[] detailInfo)
        {
            this.Category = category;
            this.ErrorCode = errorCode;
            this.Location = location;
            this.DetailInfo = detailInfo;
        }

        public CheckResultCategory Category { get; }
        public TErrorCode ErrorCode { get; }
        public TLocation Location { get; }
        public IEnumerable<string> DetailInfo { get; }

        public override string ToString()
        {
            var text = string.Join("\t", this.Category, this.ErrorCode, this.Location, this.DetailInfo.Join("\t"));
            return text;
        }
    }
}
