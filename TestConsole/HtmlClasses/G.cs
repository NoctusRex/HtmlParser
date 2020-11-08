using HtmlParser.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestConsole
{
    public class G
    {
        public string Class { get; set; }

        public IEnumerable<Use> Use { get; set; }

        [Element("Use")]
        public IEnumerable<Use> Use2 { get; set; }
    }
}
