using System;

namespace EasySharp.Win.Environments
{
    public static class EnvironmentVariables
    {
        public static readonly string LocalAppData = Environment.GetEnvironmentVariable("LOCALAPPDATA");
    }
}
