using System;

namespace EasySharpStandard.SafeCodes.Core
{
    public static class Try
    {
        #region Failed

        public static bool Failed<T>(out T a)
        {
            a = default(T);
            return false;
        }

        public static bool Failed<T1, T2>(out T1 a, out T2 b)
        {
            a = default(T1);
            b = default(T2);
            return false;
        }

        #endregion

        #region To

        public static bool To(Action action)
        {
            return To(action, out var exception);
        }

        public static bool To(Action action, out Exception exception)
        {
            try
            {
                action();
                exception = default(Exception);
                return true;
            }
            catch(Exception e)
            {
                exception = e;
                return false;
            }
        }

        public static bool To<T>(Func<T> func, out T returnValue)
        {
            try
            {
                returnValue = func();
                return true;
            }
            catch
            {
                return Failed(out returnValue);
            }
        }

        #endregion

        #region Finally

        public static T Finally<T>(Func<T> func, Action finallyAction)
        {
            try
            {
                return func();
            }
            finally
            {
                finallyAction();
            }
        }

        public static void Finally(Action func, Action finallyAction)
        {
            try
            {
                func();
            }
            finally
            {
                finallyAction();
            }
        }

        #endregion
    }
}
