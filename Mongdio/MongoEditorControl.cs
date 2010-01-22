using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MongdioLogic;

namespace Mongdio
{
	public partial class MongoEditorControl : UserControl
	{
		private MongoEditorSession _session;

		public MongoEditorControl()
		{
			InitializeComponent();
			KeyDown += MongoEditorControl_KeyDown;
			rtEditor.KeyDown += MongoEditorControl_KeyDown;

			_session = new MongoEditorSession();
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
			rtResult.Clear();
			var result = _session.Execute(command);
			rtResult.AppendText(result);
		}
	}
}
