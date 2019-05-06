using EasySharpStandard.Logs.CheckLogs.Core.Models;
using System.Diagnostics;

namespace EasySharpStandard.Logs.CheckLogs.Core.Models
{
    [DebuggerDisplay("{Category}\t{Identifacation}{CodeNumber}")]
    public class CheckResutl
    {
        public CheckResutl(CheckResultCategories category, string identifacation, int resultCode)
        {
            this.Category = category;
            this.CheckerIdentification = identifacation;
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
