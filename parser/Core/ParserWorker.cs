using AngleSharp.Html.Parser;
using System;
using System.Threading;

namespace parser.Core
{
    class ParserWorker<T> where T : class
    {
        public event Action<object, T> OnNewData;
        public event Action<object> OnCompletion;
        IParserSettings settings;
        private HtmlLoader loader;
        bool isActive;

        public ParserWorker(IParser<T> parser)
        {
            Parser = parser;
        }
        public ParserWorker(IParser<T> parser, IParserSettings settings) :this(parser)
        {
            Settings = settings;
        }

        public IParser<T> Parser { get; }
        public IParserSettings Settings
        {
            get => settings;
            set
            {
                settings = value;
                loader = new HtmlLoader(value);
            }
        }

        public void Start()
        {
            CancellationTokenSource source = new CancellationTokenSource();
            var token = source.Token;
            isActive = true;
            Work();
        }
        public void Stop()
        {
            isActive = false;
        }

        async void Work()
        {
            for(int i = Settings.Start; i <= Settings.End; i++)
            {
                if (!isActive)
                {
                    OnCompletion?.Invoke(this);
                    return;
                }

                var sourse = await loader.GetPageSourseByPageId(i);
                var domParser = new HtmlParser();
                var doc = await domParser.ParseDocumentAsync(sourse);

                var res = Parser.Parse(doc);
                OnNewData?.Invoke(this, res);
            }
            OnCompletion?.Invoke(this);
            isActive = false;
        }
    }
}
