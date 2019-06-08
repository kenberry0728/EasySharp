using System.Diagnostics;

namespace EasySharpStandard.Logs.CheckLogs.Core.Models
{
    [DebuggerDisplay("{Category}\t{CheckerIdentification}{CodeNumber}")]
    public class CheckResult
    {
        public CheckResult(CheckResultCategories category, string identification, int resultCode)
        {
            this.Category = category;
            this.CheckerIdentification = identification;
            this.CodeNumber = resultCode;
        }

        CheckResultCategories Category { get; }

        public string CheckerIdentification { get; }

        int CodeNumber { get; }

        public override string ToString()
        {
            return $"{Category}\t{CheckerIdentification}{CodeNumber}";
        }
    }
}
