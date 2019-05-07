namespace EasySharpStandard.ElementaryTypes
{
    public static class BoolExtensions
    {
        public static bool IsTrue(bool? testValue)
        {
            return testValue == true;
        }

        public static bool IsFalse(bool? testValue)
        {
            return testValue == false;
        }
    }
}
