using AngleSharp.Html.Dom;
using System.Linq;

namespace parser.Core.Shazoo
{
    class ShazooParser : IParser<string[]>
    {
        public string[] Parse(IHtmlDocument document)
        {
            var links = document.QuerySelectorAll(".entryTitle a").Select(x => x.GetAttribute("href")).ToArray();
            return document.QuerySelectorAll(".entryTitle a").Select(x => x.TextContent).ToArray();
        }
    }
}
