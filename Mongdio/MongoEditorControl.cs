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

		public string DbName
		{
			get { return _session.DataBaseName; }
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
			else if(toolStripButtonDelete.Enabled && e.KeyCode == Keys.F12)
			{
				DeleteObject();
			}
		}

		private void SaveObject()
		{
			var s = rtResult.SelectedText;
			var ret = _session.SaveObject(s);
			toolStripCommandLabel.Text = ret;
		}

		private void DeleteObject()
		{
			if(MessageBox.Show("Delete object?","Delete",MessageBoxButtons.YesNo)==DialogResult.Yes)
			{
				var s = rtResult.SelectedText;
				var ret = _session.DeleteObject(s);
				toolStripCommandLabel.Text = ret;
				rtResult.SelectedText = "";
			}
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
			int objectCount;
			if(!toolStripColoring.Checked)
			{
				var printer = new PrettyPrint();
				var result = _session.Execute(command,out objectCount,printer);
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

		private void toolStripButtonRun_Click(object sender, EventArgs e)
		{
			RunCommand();
		}

		private void toolStripButtonSave_Click(object sender, EventArgs e)
		{
			SaveObject();
		}

		private void toolStripButtonDelete_Click(object sender, EventArgs e)
		{
			DeleteObject();
		}

		private void rtResult_SelectionChanged(object sender, EventArgs e)
		{
			var s = rtResult.SelectedText;
			bool containsId;
			toolStripButtonSave.Enabled = _session.TryParseTextAsDocument(s,out containsId);
			toolStripButtonDelete.Enabled = containsId;
		}

		public void AddText(string text)
		{
			if(rtEditor.Text.Trim().Length>0)
			{
				if(!rtEditor.Text.EndsWith(Environment.NewLine))
					rtEditor.AppendText(Environment.NewLine);

				rtEditor.AppendText(Environment.NewLine);
			}
			var ss = rtEditor.Text.Length;
			rtEditor.AppendText(text);
			rtEditor.SelectionStart = ss;
			rtEditor.ScrollToCaret();
			rtEditor.Focus();
		}

	}
}
