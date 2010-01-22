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
			if(e.Alt && e.KeyCode == Keys.X)
			{
				//MessageBox.Show(this, rtEditor.SelectedText);
				var result = _session.Execute(rtEditor.SelectedText);
				rtResult.AppendText(result);
			}
		}
	}
}
