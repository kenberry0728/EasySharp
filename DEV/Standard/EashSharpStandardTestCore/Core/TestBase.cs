namespace EashSharpStandardTestCore.Core
{
    public class Test
    {
        private readonly ITest test;

        public Test(ITest test)
        {
            this.test = test;
        }

        public void Run()
        {
            this.test.Initialize();
            this.test.Run();
            this.test.CleanUp();
        }
    }
}
