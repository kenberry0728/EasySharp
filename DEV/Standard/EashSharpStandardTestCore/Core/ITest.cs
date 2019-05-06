using System;

namespace EashSharpStandardTestCore.Core
{
    public interface ITest : IDisposable
    {
        void Initialize();
        void Run();
        void CleanUp();
    }
}
