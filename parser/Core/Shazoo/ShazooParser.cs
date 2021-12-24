using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parser.Core.Shazoo
{
    class ShazooParser : IParser<List<string>>
    {
        List<string> IParser<List<string>>.Parse(IHtmlDocument document) => document.QuerySelectorAll(".entryTitle a").Select(x => x.TextContent).ToList();
    }
}
