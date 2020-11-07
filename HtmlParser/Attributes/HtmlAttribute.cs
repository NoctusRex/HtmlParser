using System;

namespace HtmlParser.Attributes
{
    public abstract class HtmlAttribute : Attribute
    {
        public string Id { get; private set; }

        public HtmlAttribute(string id)
        {
            Id = id;
        }
    }
}
