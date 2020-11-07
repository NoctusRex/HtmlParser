namespace HtmlParser.Extensions
{

    internal static class StringExtensions
    {
        public static bool SameText(this string string1, string string2) => string1.ToLower() == string2.ToLower();
    }

}
