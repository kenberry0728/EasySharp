using System;

namespace EasySharpStandard.Win.Environments
{
    public static class EnvironmentVariables
    {
        public static readonly string LocalAppData = Environment.GetEnvironmentVariable("LOCALAPPDATA");
    }
}
