using System;
using AngleSharp.Parser.Html;

namespace xTab.Tools.Helpers
{
    public static class AngleSharpParser
    {
        public static string RemoveTags(string html)
        {
            var parser = new HtmlParser();
            var document = parser.Parse(html);

            var iframes = document.GetElementsByTagName("iframe");
            var embeds = document.GetElementsByTagName("embed");
            var scripts = document.GetElementsByTagName("script");

            foreach (var item in iframes)
            {
                item.OuterHtml = String.Empty;
            }

            foreach (var item in embeds)
            {
                item.Remove();
            }

            foreach (var item in scripts)
            {
                item.Remove();
            }

            var result = document.Body.TextContent;

            return result;
        }
    }
}
