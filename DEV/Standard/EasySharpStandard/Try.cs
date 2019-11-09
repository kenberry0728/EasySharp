using System;

namespace EasySharp
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
            return To(action, out _);
        }

        public static bool To(Action action, out Exception exception)
        {
            try
            {
                action();
                exception = default;
                return true;
            }
            catch (Exception e)
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

        public static T Finally<T>(this Func<T> func, Action finallyAction)
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

        public static void Finally(this Action func, Action finallyAction)
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


        public static bool ToFinally(Action action, Action finallyAction)
        {
            return ToFinally(action, finallyAction, out _);
        }

        public static bool ToFinally(Action action, Action finallyAction, out Exception exception)
        {
            try
            {
                action();
                exception = default;
                return true;
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
            finally
            {
                finallyAction();
            }
        }

        public static bool ToFinally<T>(Func<T> func, Action finallyAction, out T returnValue)
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
            finally
            {
                finallyAction();
            }
        }
    }
}
