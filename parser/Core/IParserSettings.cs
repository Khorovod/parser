namespace parser.Core
{
    interface IParserSettings
    {
        string BaseUrl { get; set; }
        string PostFix { get; set; }
        int Start { get; set; }
        int End { get; set; }
    }
}
