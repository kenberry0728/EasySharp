namespace EasySharp
{
    public static class Predicates
    {
        public static bool True()
        {
            return true;
        }

        public static bool True<T>(T instance)
        {
            return true;
        }

        public static bool True<T1, T2>(T1 instance1, T2 instance2)
        {
            return true;
        }

        public static bool True<T1, T2, T3>(T1 instance1, T2 instance2, T3 instance3)
        {
            return true;
        }

        public static bool False()
        {
            return false;
        }

        public static bool False<T>(T instance)
        {
            return false;
        }

        public static bool False<T1, T2>(T1 instance1, T2 instance2)
        {
            return false;
        }

        public static bool False<T1, T2, T3>(T1 instance1, T2 instance2, T3 instance3)
        {
            return false;
        }
    }
}
