﻿using AngleSharp.Html.Parser;
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

        public void Start(CancellationToken token)
        {
            Work(token);
        }

        async void Work(CancellationToken token)
        {
            for(int i = Settings.Start; i <= Settings.End; i++)
            {
                if (token.IsCancellationRequested)
                {
                    OnCompletion?.Invoke(this);
                    return;
                }
                string sourse = null;
                try
                {
                    sourse = await loader.GetPageSourseByPageId(i, token);
                }
                catch(OperationCanceledException ex)
                {
                    OnCompletion?.Invoke(ex.Message);
                }
                var domParser = new HtmlParser();
                var doc = await domParser.ParseDocumentAsync(sourse);

                var res = Parser.Parse(doc);
                OnNewData?.Invoke(this, res);
            }
            OnCompletion?.Invoke(this);
        }
    }
}
