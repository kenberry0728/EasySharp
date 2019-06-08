using System;
using System.Threading;

namespace EasySharpStandard.SafeCodes.Core
{
    public static class Retry
    {
        public static void Run(Action action, int maxRetry = 10, int intervalMillisecond = 200)
        {
            if (Try.To(action))
            {
                return;
            }

            for (int i = 1; i < maxRetry; i++)
            {
                Thread.Sleep(intervalMillisecond);
                if (Try.To(action))
                {
                    break;
                }
            }
        }

        public static T Run<T>(Func<T> func, int maxRetry = 10, int intervalMillisecond = 200)
        {
            if (Try.To(func, out var returnValue))
            {
                return returnValue;
            }

            for (int i = 1; i < maxRetry; i++)
            {
                Thread.Sleep(intervalMillisecond);
                if (Try.To(func, out returnValue))
                {
                    return returnValue;
                }
            }

            return default;
        }
    }
}
