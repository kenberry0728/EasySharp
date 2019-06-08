using EasySharpStandardTestCore.Core;
using System;
using System.Diagnostics;
using System.IO;

namespace EasySharpStandardConsoleTest.ConsoleTests.Implementations
{
    internal abstract class ConsoleTestBase : ITest
    {
        private readonly MemoryStream outputMemoryStream = new MemoryStream();
        private readonly MemoryStream inputMemoryStream = new MemoryStream();
        private readonly StreamWriter streamWriter;
        private readonly StreamReader streamReader;
        private readonly TextWriter originalOut;
        private readonly TextReader originalIn;

        public ConsoleTestBase()
        {
            this.originalOut = Console.Out;
            this.originalIn = Console.In;
            this.streamWriter = new StreamWriter(outputMemoryStream) { AutoFlush = false };
            this.streamReader = new StreamReader(inputMemoryStream);
        }

        public void Initialize()
        {
            Console.SetOut(this.streamWriter);
            Console.SetIn(this.streamReader);
        }

        public abstract void Run();

        public void CleanUp()
        {
            Debug.Assert(false, "Not working.");
            Console.SetOut(this.originalOut);
            Console.SetIn(this.originalIn);
        }

        public void Dispose()
        {
            this.streamWriter.Close();
            this.streamReader.Close();

            this.outputMemoryStream.Close();
            this.inputMemoryStream.Close();
        }

        public string ReadToEnd()
        {
            this.streamWriter.Flush();
            this.streamWriter.BaseStream.Seek(0, SeekOrigin.Begin);
            var sr = new StreamReader(outputMemoryStream);
            var result = sr.ReadToEnd();
            return result;
        }
    }
}
