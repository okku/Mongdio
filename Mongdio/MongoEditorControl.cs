using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mongdio.code;
using MongdioLogic;
using MongdioLogic.db;

namespace Mongdio
{
	public partial class MongoEditorControl : UserControl
	{
		private MongoEditorSession _session;

		public MongoEditorControl(string name)
		{
			InitializeComponent();
			KeyDown += MongoEditorControl_KeyDown;
			rtEditor.KeyDown += MongoEditorControl_KeyDown;
			rtResult.KeyDown += MongoEditorControl_KeyDown;
			rtEditor.SelectionTabs = (new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }).Select(x => x * 32).ToArray();
			rtResult.SelectionTabs = (new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }).Select(x => x * 32).ToArray();

			_session = new MongoEditorSession(name);
		}

		void MongoEditorControl_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F5)
			{
				RunCommand();
			}
			else if(toolStripButtonSave.Enabled && e.KeyCode == Keys.F6)
			{
				SaveObject();
			}
		}

		private void SaveObject()
		{
			var s = rtResult.SelectedText;
			var ret = _session.SaveObject(s);
			toolStripCommandLabel.Text = ret;
		}

		private void RunCommand()
		{
			string command;
			if(rtEditor.SelectedText.Length > 0)
				command = rtEditor.SelectedText;
			else
				command = rtEditor.Text;

			RunCommand(command);
		}

		private void RunCommand(string command)
		{
			var bgw = new BackgroundWorker();
			bgw.DoWork += bgw_DoWork;
			bgw.ProgressChanged += bgw_ProgressChanged;
			bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
			bgw.WorkerReportsProgress = true;
			bgw.WorkerSupportsCancellation = true;
			bgw.RunWorkerAsync(command);
		}

		void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			throw new NotImplementedException();
		}

		void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			e.ProgressPercentage

			toolStripCommandLabel.Text = string.Format("{0} objects", objectCount);
		}

		void bgw_DoWork(object sender, DoWorkEventArgs e)
		{
			var bgw = sender as BackgroundWorker;
			var command = e.Argument as string;

			int setSize = 10;
			int offset = 0;
			rtResult.Clear();
			while(true)
			{
				int objectCount;
				if(!toolStripColoring.Checked)
				{
					var printer = new PrettyPrint();
					var result = _session.Execute(command, out objectCount, printer, setSize, offset);
					//rtResult.AppendText(result);
					bgw.ReportProgress();
				}
				else
				{
					var printer = new RTFPrettyPrinter();
					var rtfResult = _session.Execute(command, out objectCount, printer, setSize, offset);
					//RTFHelper.SetRTF(rtResult, rtfResult, RTFHelper.COLOR_TABLE);
				}

				offset += setSize;
			}
		}
/*
		private void RunCommand(string command)
		{
			int objectCount;
			if(!toolStripColoring.Checked)
			{
				var printer = new PrettyPrint();
				var result = _session.Execute(command, out objectCount, printer);
				rtResult.Text = result;
			}
			else
			{
				var printer = new RTFPrettyPrinter();
				var rtfResult = _session.Execute(command, out objectCount, printer);
				RTFHelper.SetRTF(rtResult, rtfResult, RTFHelper.COLOR_TABLE);
			}
			toolStripCommandLabel.Text = string.Format("{0} objects", objectCount);
		}
*/

		private void toolStripButtonRun_Click(object sender, EventArgs e)
		{
			RunCommand();
		}

		private void toolStripButtonSave_Click(object sender, EventArgs e)
		{
			SaveObject();
		}

		private void rtResult_SelectionChanged(object sender, EventArgs e)
		{
			var s = rtResult.SelectedText;
			toolStripButtonSave.Enabled = _session.TryParseTextAsDocument(s);
		}

	}
}
