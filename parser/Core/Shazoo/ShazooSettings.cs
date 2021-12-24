namespace parser.Core.Shazoo
{
    class ShazooSettings : IParserSettings
    {
        public string BaseUrl { get; set; } = "https://shazoo.ru/news";
        public string PostFix { get; set; } = "?page=";
        public int Start { get; set; }
        public int End { get; set; }
    }
}
