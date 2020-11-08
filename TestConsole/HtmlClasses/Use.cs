using System;
using HtmlParser.Attributes;

namespace TestConsole
{
    public class Use
    {
        [Attribute("xlink:href")]
        public string CrossLink { get; set; }

        public string Content { get; set; }

        public int X { get; set; }

        [Ignore()]
        public int Y { get; set; }

        public string Fill { get; set; }

    }
}
