using System.Collections.Generic;

namespace HtmlParser.Constants
{
    public sealed class Html
    {
        private Html() { }

        public static IEnumerable<string> VoidElements => new string[] {"area", "base", "br", "col", "hr", "img", "input", "link", "meta", "param", "command", "keygen", "source"};

        public static string ClosingTagPrefix => "</";
        public static string ClosingTagInlineSuffix => "/>";
        public static string OpeningTagPrefix => "<";
        public static string TagSuffix => ">";
    }
}
