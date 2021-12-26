using System.Windows.Forms;
using System;
using parser.Core;
using parser.Core.Shazoo;
using System.Threading;

namespace parser
{
    public partial class Form1 : Form
    {
        ParserWorker<string[]> _parserWorker;
        CancellationTokenSource _source;

        public Form1()
        {
            InitializeComponent();
            _parserWorker = new ParserWorker<string[]>(new ShazooParser());

            _parserWorker.OnNewData += _parserWorker_OnNewData;
            _parserWorker.OnCompletion += _parserWorker_OnCompletion;
        }

        private void _parserWorker_OnCompletion(object obj)
        {
            if (!_source.IsCancellationRequested)
            {
                ListBox.Items.Add("-------------------------------------------все сделано----------------------------------------------");
            }
            else
            {
                ListBox.Items.Add("---------------------------------------------отмена------------------------------------------------");
            }
        }

        private void _parserWorker_OnNewData(object arg1, string[] arg2)
        {
            ListBox.Items.AddRange(arg2);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            _source = new CancellationTokenSource();
            _parserWorker.Settings = new ShazooSettings((int)startPg.Value, (int)endPg.Value);
            _parserWorker.Start(_source.Token);
        }

        private void endButton_Click(object sender, EventArgs e)
        {
            _source?.Cancel();
            _source?.Dispose();
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            ListBox.Items.Clear();
        }
    }
}
