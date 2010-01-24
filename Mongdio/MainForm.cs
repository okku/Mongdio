using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MongdioLogic.db;
using MongoDB.Driver;

namespace Mongdio
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			Load += MainForm_Load;
			treeViewMenu.DoubleClick += treeViewMenu_DoubleClick;
		}

		void treeViewMenu_DoubleClick(object sender, EventArgs e)
		{
			var tn = treeViewMenu.SelectedNode;
			if(tn!=null)
			{
				var dbName = tn.Tag as string;
				if(dbName != null)
					OpenNewQueryTab(dbName);
			}
		}

		private void OpenNewQueryTab(string name)
		{
			var tp = new TabPage(name);
			var mec = new MongoEditorControl(name);
			mec.Dock = DockStyle.Fill;
			tp.Controls.Add(mec);
			tabControl1.TabPages.Add(tp);
		}

		void MainForm_Load(object sender, EventArgs e)
		{
			var dbs = MDB.GetDatabases();
			var root = new TreeNode("MongoDB");
			foreach(var db in dbs)
			{
				var dbName = db["name"].ToString();
				var dbNode = new TreeNode(dbName) {Tag = dbName};
				root.Nodes.Add(dbNode);
				var collections = MDB.GetCollections(dbName);
				dbNode.Nodes.AddRange(collections.Where(x=>x["name"].ToString().Count(y=>y=='.')<2).Select(x => new TreeNode(x["name"].ToString())).ToArray());
			}

			//root.Nodes.AddRange(dbs.Select(x => new TreeNode(x["name"].ToString()){Tag = x["name"]}).ToArray());
			treeViewMenu.BeginUpdate();
			treeViewMenu.Nodes.Add(root);
			root.Expand();
			treeViewMenu.EndUpdate();
		}
	}
}
