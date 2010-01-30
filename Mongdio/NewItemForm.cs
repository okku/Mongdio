using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mongdio
{
	public partial class NewItemForm : Form
	{
		public NewItemForm()
		{
			InitializeComponent();
		}

		public NewItemForm(string title, string label)
		{
			InitializeComponent();
			Text = title;
			lblText.Text = label;
		}

		public string EnteredValue
		{
			get { return tbValue.Text;  }
		}
	}
}
