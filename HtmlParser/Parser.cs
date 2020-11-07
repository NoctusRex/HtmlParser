using HtmlParser.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using HtmlParser.Constants;
using System.Linq;
using HtmlParser.Extensions;
using System.Diagnostics;

namespace HtmlParser
{
    /// <summary>
    /// Parses Html - requires it to be valid and to have the surrounding <html></html> tag
    /// </summary>
    public class Parser
    {

        /// <summary>
        /// Opens the uri and downloads the loaded html
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public HtmlElement Parse(string uri) => Parse(new Uri(uri));

        /// <summary>
        /// Opens the uri and downloads the loaded html
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public HtmlElement Parse(Uri uri)
        {
            using WebClient client = new WebClient();

            return ParseRawHtml(client.DownloadString(uri));
        }

        /// <summary>
        /// Converts the RawHtml into an HtmlElement
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public HtmlElement ParseRawHtml(string html) => new HtmlElement { Id = "root", Elements = ParseElements(html, null) };

        /// <summary>
        /// Once 2 weeks have passed my knowledge of whats happening here will have passed away as well.
        /// </summary>
        /// <param name="html"></param>
        /// <param name="lastElement"></param>
        /// <returns></returns>
        private IEnumerable<HtmlElement> ParseElements(string html, HtmlElement lastElement)
        {
            List<HtmlElement> elements = new List<HtmlElement>();
            int lastProcessedIndex = 0;

            while (lastProcessedIndex < html.Length)
            {
                HtmlElement element = new HtmlElement();

                if (!html.Contains("<") || !html.Contains("</") || html.Count(x => x == '>') < 2)
                {
                    lastElement.Content = html;
                    return null;
                }

                string openingTag = GetOpeningTag(html, lastProcessedIndex, out lastProcessedIndex);
                string openingTagName = GetRawTagName(openingTag);
                string closingTag = Html.ClosingTagPrefix + openingTagName + Html.TagSuffix;

                element.Id = openingTagName;
                element.Attributes = ParseAttributes(openingTag);

                if (Html.VoidElements.Contains(openingTagName))
                {
                    lastProcessedIndex++;
                }
                else
                {
                    int closingTagStartIndex = GetClosingTagStartIndex(html, lastProcessedIndex + 1 - openingTag.Length, Html.OpeningTagPrefix + openingTagName + Html.TagSuffix, closingTag);
                    element.Elements = ParseElements(html.Substring(lastProcessedIndex + 1, closingTagStartIndex - 1 - lastProcessedIndex), element);
                    lastProcessedIndex = closingTagStartIndex + closingTag.Length;

                    if (!html.Substring(lastProcessedIndex).Contains("<") ||
                        !html.Substring(lastProcessedIndex).Contains("</") ||
                        html.Substring(lastProcessedIndex).Count(x => x == '>') < 2)
                        lastProcessedIndex = html.Length;
                }

                elements.Add(element);
            }

            return elements;
        }

        private string GetOpeningTag(string html, int startIndex, out int endIndex)
        {
            int stopIndex = -1;
            bool isComment = false;

            for (int i = startIndex; i < html.Length; i++)
            {
                if (i > 0 && html[i - 1] == '<' && html[i] == '!') isComment = true;
                if (html[i] != '<' && html[i] != '>') continue;

                if (html[i] == '<') startIndex = i;
                if (html[i] == '>')
                {
                    stopIndex = i;
                    if (!isComment && (html[startIndex + 1] != '/')) break;// reset if a comment was found or a closing tag was found, that was not opend

                    isComment = false;
                    startIndex = i;
                    stopIndex = -1;
                }
            }

            if (stopIndex == -1) throw new Exception($"Opening tag could not be found in '{html}' starting at char '{startIndex}'.");

            endIndex = stopIndex;
            return html.Substring(startIndex, stopIndex - startIndex + 1);
        }

        private int GetClosingTagStartIndex(string html, int startIndex, string openingTag, string closingTag)
        {
            // Stack to store opening tags.  
            Stack st = new Stack();

            // Traverse through string starting from given index.   
            for (int i = startIndex; i < html.Length; i++)
            {
                // If current character is an opening tag push it in stack.  
                if (html[i] == '<')
                {
                    if (i < html.Length && html[i + 1] != '/')
                    {
                        if (GetNextTagWithoutAttributes(html.Substring(i)).SameText(openingTag))
                        {
                            st.Push((int)html[i]);
                            continue;
                        }
                    }
                    else // if is closing tag pop from stack
                    {
                        if (GetNextTagWithoutAttributes(html.Substring(i)).SameText(closingTag))
                        {
                            st.Pop();
                            if (st.Count == 0) return i;
                        }
                    }
                }

            }

            throw new Exception($"Closing tag '{closingTag}' to opening tag '{openingTag}' could not be found in '{html}'.");
        }

        private string GetNextTagWithoutAttributes(string html)
        {
            int startIndex = -1, stopIndex = -1;

            for (int i = 0; i < html.Length; i++)
            {
                if (html[i] != '<' && html[i] != '>') continue;

                if (html[i] == '<') startIndex = i;
                if (html[i] == '>') { stopIndex = i; break; }
            }

            if (startIndex == -1 || stopIndex == -1) throw new Exception($"Tag could not be found in '{html}'.");

            return Html.OpeningTagPrefix + GetRawTagName(html.Substring(startIndex, stopIndex - startIndex + 1)) + Html.TagSuffix;
        }

        private string GetRawTagName(string tag) => tag.Substring(1, tag.Contains(" ") ? tag.Split(' ').First().Length : tag.Length - 2).Trim();

        private IEnumerable<HtmlAttribute> ParseAttributes(string parentTag)
        {
            string[] foundAttributes = parentTag.TrimStart('<').TrimEnd('>').Split("\" ");

            if (foundAttributes is null || foundAttributes.Count() == 0) return null;

            List<HtmlAttribute> attributes = new List<HtmlAttribute>();

            // remove tag name
            foundAttributes[0] = string.Join(' ', foundAttributes[0].Split(' ').Skip(1));

            foreach (string attribute in foundAttributes)
            {
                if (string.IsNullOrEmpty(attribute) || !attribute.Contains("=")) continue;
                attributes.Add(new HtmlAttribute()
                {
                    Id = attribute.Split('=')[0],
                    Value = attribute.Split('=')[1]
                });
            }

            if (attributes.Count() == 0) return null;

            return attributes;
        }

    }
}
