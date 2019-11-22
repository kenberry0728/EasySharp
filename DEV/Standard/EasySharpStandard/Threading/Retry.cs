using System;
using System.Threading;

namespace EasySharp.Threading
{
    public static class Retry
    {
        public static bool Run(
            Action action,
            int maxRetry = 10,
            int intervalMillisecond = 200)
        {
            if (Try.To(action))
            {
                return true;
            }

            for (int i = 1; i < maxRetry; i++)
            {
                Thread.Sleep(intervalMillisecond);
                if (Try.To(action))
                {
                    break;
                }
            }

            return false;
        }

        public static T Run<T>(
            Func<T> func, 
            int maxRetry = 10, 
            int intervalMillisecond = 200)
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

        public static bool Until(
            Func<bool> endPredicate,
            int maxRetry = 10,
            int intervalMillisecond = 200)
        {
            if (Try.To(endPredicate, out var returnValue) && returnValue)
            {
                return true;
            }

            for (int i = 1; i < maxRetry; i++)
            {
                Thread.Sleep(intervalMillisecond);
                if (Try.To(endPredicate, out returnValue) && returnValue)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool Until<T>(
            Func<T> func,
            Func<T, bool> endPredicate,
            out T returnValue,
            int maxRetry = 10,
            int intervalMillisecond = 200)
        {
            if (Try.To(func, out returnValue))
            {
                if (endPredicate(returnValue))
                {
                    return true;
                }
            }

            for (int i = 1; i < maxRetry; i++)
            {
                Thread.Sleep(intervalMillisecond);
                if (Try.To(func, out returnValue))
                {
                    if (endPredicate(returnValue))
                    {
                        return true;
                    }
                }
            }

            return Try.Failed(out returnValue);
        }
    }
}
