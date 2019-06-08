using System;

namespace EasySharpStandardTestCore.Core
{
    public interface ITest : IDisposable
    {
        void Initialize();
        void Run();
        void CleanUp();
    }
}
