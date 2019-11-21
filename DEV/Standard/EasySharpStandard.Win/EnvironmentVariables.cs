using System;

namespace EasySharp.Win.Environments
{
    public static class EnvironmentVariables
    {
        public static readonly string LocalAppData = Environment.GetEnvironmentVariable("LOCALAPPDATA");

        public static readonly string ProgramFiles86 = Environment.GetEnvironmentVariable("ProgramFiles(x86)");
    }
}
