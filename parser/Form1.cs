using System.Windows.Forms;
using System;
using parser.Core;
using parser.Core.Shazoo;

namespace parser
{
    public partial class Form1 : Form
    {
        ParserWorker<string[]> _parserWorker;
        public Form1()
        {
            InitializeComponent();
            _parserWorker = new ParserWorker<string[]>(new ShazooParser());

            _parserWorker.OnNewData += _parserWorker_OnNewData;
            _parserWorker.OnCompletion += _parserWorker_OnCompletion;
        }

        private void _parserWorker_OnCompletion(object obj)
        {
            MessageBox.Show("Done");
        }

        private void _parserWorker_OnNewData(object arg1, string[] arg2)
        {
            ListBox.Items.AddRange(arg2);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            _parserWorker.Settings = new ShazooSettings((int)startPg.Value, (int)endPg.Value);
            _parserWorker.Start();
        }

        private void endButton_Click(object sender, EventArgs e)
        {
            _parserWorker.Stop();
        }

    }
}
