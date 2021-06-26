using EasySharp.Logs.Check.Core.Models;

namespace EasySharp.Logs.Check.Core
{
    public interface ICheckLogger<TErrorCode, TLocation>
    {
        void Write(CheckResult<TErrorCode, TLocation> result, string message);
    }
}
