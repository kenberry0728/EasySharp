using System;

namespace EasySharp
{
    public static class PredicateExtensions
    {
        public static Func<T, bool> TrueIfNull<T>(this Func<T, bool> func)
        {
            return func ?? Delegates.True;
        }

        public static Func<T1, T2, bool> TrueIfNull<T1, T2>(this Func<T1, T2, bool> func)
        {
            return func ?? Delegates.True;
        }

        public static Func<T1, T2, T3, bool> TrueIfNull<T1, T2, T3>(this Func<T1, T2, T3, bool> func)
        {
            return func ?? Delegates.True;
        }

        public static Func<T, bool> FalseIfNull<T>(this Func<T, bool> func)
        {
            return func ?? Delegates.False;
        }

        public static Func<T1, T2, bool> FalseIfNull<T1, T2>(this Func<T1, T2, bool> func)
        {
            return func ?? Delegates.False;
        }

        public static Func<T1, T2, T3, bool> FalseIfNull<T1, T2, T3>(this Func<T1, T2, T3, bool> func)
        {
            return func ?? Delegates.False;
        }
    }
}
