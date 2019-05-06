using System;

namespace EasySharpStandard.SafeCodes.Core
{
    public static class Try
    {
        #region Failed

        public static bool Failed<T>(out T a)
        {
            a = default;
            return false;
        }

        public static bool Failed<T1, T2>(out T1 a, out T2 b)
        {
            a = default;
            b = default;
            return false;
        }

        #endregion

        #region To

        public static bool To(Action action)
        {
            try
            {
                action();
                return true;
            }
            catch
            {
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
    }
}
