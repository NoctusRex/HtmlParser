namespace TestConsole
{
    public class Svg
    {
        public string Class { get; set; }
        public string Xmlns { get; set; }
        [HtmlParser.Attributes.Attribute("xmlns:xlink")]
        public string XmlnsXlink { get; set; }
        public string ViewBox { get; set; }
        public string PreserveAspectRatio { get; set; }
        [HtmlParser.Attributes.Attribute("shape-rendering")]
        public string ShapeRendering { get; set; }

        public Defs Defs { get; set; }
        public G G { get; set; }
    }
}
