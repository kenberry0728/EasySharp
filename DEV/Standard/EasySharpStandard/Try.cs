using System;

namespace EasySharp
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Ignore VB.")]
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

        public static Result To(Action action)
        {
            try
            {
                action();
                return new Ok();
            }
            catch (Exception e)
            {
                return new Err(e);
            }
        }

        public static Result<T> To<T>(Func<T> func)
        {
            try
            {
                return new Ok<T>(func());
            }
            catch (Exception exception)
            {
                return new Err<T>(exception);
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

        public static Result ToFinally(Action action, Action finallyAction)
        {
            try
            {
                action();
                return new Ok();
            }
            catch (Exception e)
            {
                return new Err(e);
            }
            finally
            {
                finallyAction();
            }
        }

        public static Result<T> ToFinally<T>(Func<T> func, Action finallyAction)
        {
            try
            {
                return new Ok<T>(func());
            }
            catch (Exception exception)
            {
                return new Err<T>(exception);
            }
            finally
            {
                finallyAction();
            }
        }
    }
}
