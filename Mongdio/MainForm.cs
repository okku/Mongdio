using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mongdio.code;
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
			treeViewMenu.MouseUp += treeViewMenu_MouseUp;
		}

		void treeViewMenu_MouseUp(object sender, MouseEventArgs e)
		{
			treeViewMenu.SelectedNode = treeViewMenu.GetNodeAt(e.X, e.Y);
		}

		void treeViewMenu_DoubleClick(object sender, EventArgs e)
		{
			var dbNode = treeViewMenu.SelectedNode as DbNode;
			if(dbNode != null)
			{
				OpenNewQueryTab(dbNode.DbName);
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
			RefreshTreeViewMenu();
		}

		private void RefreshTreeViewMenu()
		{
			var dbs = MDB.GetDatabases();
			var root = new RootNode("MongoDB",ctxMenuStripRoot);
			foreach(var db in dbs)
			{
				var dbName = db["name"].ToString();
				var dbNode = new DbNode(dbName, ctxMenuStripDb);
				root.Nodes.Add(dbNode);
				var collections = MDB.GetCollections(dbName);
				dbNode.Nodes.AddRange(collections.Where(x=>x["name"].ToString().Count(y=>y=='.')<2).Select(x => new CollectionNode(x["name"].ToString(),ctxMenuStripCol)).ToArray());
			}

			treeViewMenu.BeginUpdate();
			treeViewMenu.Nodes.Clear();
			treeViewMenu.Nodes.Add(root);
			root.Expand();
			treeViewMenu.EndUpdate();
		}

		private void howToToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AboutBox ab = new AboutBox();
			ab.Show();
		}

		private void dropToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var node = treeViewMenu.SelectedNode;
			if(node is DbNode)
			{
				var dbNode = node as DbNode;
				var d = MessageBox.Show(string.Format("Are you sure you want to drop database '{0}'", dbNode.DbName),
				                        "Drop database", MessageBoxButtons.YesNo) == DialogResult.Yes;
				if(d)
				{
					MDB.DropDatabase(dbNode.DbName);
					dbNode.Remove();
				}
			}
			else if(node is CollectionNode)
			{
				var colNode = node as CollectionNode;
				var d = MessageBox.Show(string.Format("Are you sure you want to drop collection '{0}'", colNode.CollectionName),
										"Drop collection", MessageBoxButtons.YesNo) == DialogResult.Yes;
				if(d)
				{
					var dbNode = node.Parent as DbNode;
					MDB.DropCollection(dbNode.DbName,colNode.CollectionName);
					colNode.Remove();
				}
			}
		}

		private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RefreshTreeViewMenu();
		}

		private void createToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var node = treeViewMenu.SelectedNode;
			if(node is DbNode)
			{
				var dbNode = node as DbNode;
				var nif = new NewItemForm("Create new collection", "Collection name");
				if(nif.ShowDialog() == DialogResult.OK)
				{
					MDB.CreateCollection(dbNode.DbName, nif.EnteredValue);
					dbNode.Nodes.Add(new CollectionNode(dbNode.DbName + "." + nif.EnteredValue,ctxMenuStripCol));
				}
			}
			else if(node is RootNode)
			{
				var nif = new NewItemForm("Create new database", "Database name");
				if(nif.ShowDialog() == DialogResult.OK)
				{
					MDB.CreateDatabase(nif.EnteredValue);
					node.Nodes.Add(new DbNode(nif.EnteredValue, ctxMenuStripDb));
				}
			}
		}

		private void newQueryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var dbNode = treeViewMenu.SelectedNode as DbNode;
			if(dbNode != null)
			{
				OpenNewQueryTab(dbNode.DbName);
			}
		}
	}
}
