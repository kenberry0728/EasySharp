using System;
using System.Threading;

namespace EasySharp.Threading
{
    public static class Retry
    {
        public static Result Run(
            Action action,
            int maxRetry = 10,
            int intervalMillisecond = 200)
        {
            var result = Try.To(action);
            if (result.Ok)
            {
                return result;
            }

            for (int i = 1; i < maxRetry; i++)
            {
                Thread.Sleep(intervalMillisecond);
                result = Try.To(action);
                if (result.Ok)
                {
                    break;
                }
            }

            return result;
        }

        public static T Run<T>(
            Func<T> func, 
            int maxRetry = 10, 
            int intervalMillisecond = 200)
        {
            var result = Try.To(func);
            if (result.Ok)
            {
                return result.Value;
            }

            for (int i = 1; i < maxRetry; i++)
            {
                Thread.Sleep(intervalMillisecond);
                result = Try.To(func);

                if (result.Ok)
                {
                    return result.Value;
                }
            }

            return default;
        }

        public static bool Until(
            Func<bool> endPredicate,
            int maxRetry = 10,
            int intervalMillisecond = 200)
        {
            var result = Try.To(endPredicate);
            if (result.Ok && result.Value)
            {
                return true;
            }

            for (int i = 1; i < maxRetry; i++)
            {
                Thread.Sleep(intervalMillisecond);
                result = Try.To(endPredicate);
                if (result.Ok && result.Value)
                {
                    return true;
                }
            }

            return false;
        }

        public static Result<T> Until<T>(
            Func<T> func,
            Func<T, bool> endPredicate,
            int maxRetry = 10,
            int intervalMillisecond = 200)
        {
            var result = Try.To(func);
            if (result.Ok && endPredicate(result.Value))
            {
                return result;
            }

            for (int i = 1; i < maxRetry; i++)
            {
                Thread.Sleep(intervalMillisecond);
                result = Try.To(func);

                if (result.Ok && endPredicate(result.Value))
                {
                    return result;
                }
            }

            return result;
        }
    }
}
