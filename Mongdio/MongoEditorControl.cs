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

			_session = new MongoEditorSession(name);
		}

		void MongoEditorControl_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F5)
			{
				//MessageBox.Show(this, rtEditor.SelectedText);
				string command;
				if(rtEditor.SelectedText.Length > 0)
					command = rtEditor.SelectedText;
				else
					command = rtEditor.Text;

				RunCommand(command);
			}
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
	}
}
