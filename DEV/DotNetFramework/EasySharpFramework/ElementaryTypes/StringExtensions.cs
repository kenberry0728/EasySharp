namespace EasySharpStandard.ElementaryTypes
{
    public static class StringExtensions
    {
        public static string Indent(this string target, int level)
        {
            return new string('\t', level) + target;
        }
    }
}
