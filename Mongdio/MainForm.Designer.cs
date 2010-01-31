namespace Mongdio
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.howToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.treeViewMenu = new System.Windows.Forms.TreeView();
			this.ctxMenuStripDb = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.dropToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.newQueryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ctxMenuStripCol = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
			this.ctxMenuStripRoot = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.oneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.byIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.countToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.limitTo50ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.ctxMenuStripDb.SuspendLayout();
			this.ctxMenuStripCol.SuspendLayout();
			this.ctxMenuStripRoot.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(876, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.openToolStripMenuItem1,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
			this.openToolStripMenuItem.Text = "New";
			// 
			// openToolStripMenuItem1
			// 
			this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
			this.openToolStripMenuItem1.Size = new System.Drawing.Size(111, 22);
			this.openToolStripMenuItem1.Text = "Open";
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
			this.saveToolStripMenuItem.Text = "Save";
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.howToToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// howToToolStripMenuItem
			// 
			this.howToToolStripMenuItem.Name = "howToToolStripMenuItem";
			this.howToToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.howToToolStripMenuItem.Text = "How to";
			this.howToToolStripMenuItem.Click += new System.EventHandler(this.howToToolStripMenuItem_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 24);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.treeViewMenu);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
			this.splitContainer1.Size = new System.Drawing.Size(876, 592);
			this.splitContainer1.SplitterDistance = 177;
			this.splitContainer1.TabIndex = 2;
			// 
			// treeViewMenu
			// 
			this.treeViewMenu.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeViewMenu.Location = new System.Drawing.Point(0, 0);
			this.treeViewMenu.Name = "treeViewMenu";
			this.treeViewMenu.Size = new System.Drawing.Size(177, 592);
			this.treeViewMenu.TabIndex = 0;
			// 
			// ctxMenuStripDb
			// 
			this.ctxMenuStripDb.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newQueryToolStripMenuItem,
            this.toolStripSeparator4,
            this.createToolStripMenuItem,
            this.refreshToolStripMenuItem,
            this.toolStripSeparator1,
            this.dropToolStripMenuItem});
			this.ctxMenuStripDb.Name = "contextMenuTV";
			this.ctxMenuStripDb.Size = new System.Drawing.Size(168, 104);
			// 
			// dropToolStripMenuItem
			// 
			this.dropToolStripMenuItem.Name = "dropToolStripMenuItem";
			this.dropToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.dropToolStripMenuItem.Text = "Drop Database";
			this.dropToolStripMenuItem.Click += new System.EventHandler(this.dropToolStripMenuItem_Click);
			// 
			// createToolStripMenuItem
			// 
			this.createToolStripMenuItem.Name = "createToolStripMenuItem";
			this.createToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.createToolStripMenuItem.Text = "Create Collection";
			this.createToolStripMenuItem.Click += new System.EventHandler(this.createToolStripMenuItem_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(695, 592);
			this.tabControl1.TabIndex = 0;
			// 
			// refreshToolStripMenuItem
			// 
			this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
			this.refreshToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			this.refreshToolStripMenuItem.Text = "Refresh";
			this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(154, 6);
			// 
			// newQueryToolStripMenuItem
			// 
			this.newQueryToolStripMenuItem.Name = "newQueryToolStripMenuItem";
			this.newQueryToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.newQueryToolStripMenuItem.Text = "New Query";
			this.newQueryToolStripMenuItem.Click += new System.EventHandler(this.newQueryToolStripMenuItem_Click);
			// 
			// ctxMenuStripCol
			// 
			this.ctxMenuStripCol.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findToolStripMenuItem,
            this.toolStripMenuItem2,
            this.toolStripSeparator2,
            this.toolStripMenuItem4});
			this.ctxMenuStripCol.Name = "contextMenuTV";
			this.ctxMenuStripCol.Size = new System.Drawing.Size(158, 98);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(157, 22);
			this.toolStripMenuItem2.Text = "Drop Collection";
			this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(136, 6);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(139, 22);
			this.toolStripMenuItem4.Text = "Refresh";
			// 
			// ctxMenuStripRoot
			// 
			this.ctxMenuStripRoot.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem7,
            this.toolStripSeparator3,
            this.toolStripMenuItem8});
			this.ctxMenuStripRoot.Name = "contextMenuTV";
			this.ctxMenuStripRoot.Size = new System.Drawing.Size(168, 54);
			// 
			// toolStripMenuItem7
			// 
			this.toolStripMenuItem7.Name = "toolStripMenuItem7";
			this.toolStripMenuItem7.Size = new System.Drawing.Size(167, 22);
			this.toolStripMenuItem7.Text = "Create Database";
			this.toolStripMenuItem7.Click += new System.EventHandler(this.toolStripMenuItem7_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(164, 6);
			// 
			// toolStripMenuItem8
			// 
			this.toolStripMenuItem8.Name = "toolStripMenuItem8";
			this.toolStripMenuItem8.Size = new System.Drawing.Size(167, 22);
			this.toolStripMenuItem8.Text = "Refresh";
			this.toolStripMenuItem8.Click += new System.EventHandler(this.toolStripMenuItem8_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(164, 6);
			// 
			// findToolStripMenuItem
			// 
			this.findToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oneToolStripMenuItem,
            this.limitTo50ToolStripMenuItem,
            this.allToolStripMenuItem,
            this.byIdToolStripMenuItem,
            this.countToolStripMenuItem});
			this.findToolStripMenuItem.Name = "findToolStripMenuItem";
			this.findToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			this.findToolStripMenuItem.Text = "Find";
			// 
			// oneToolStripMenuItem
			// 
			this.oneToolStripMenuItem.Name = "oneToolStripMenuItem";
			this.oneToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.oneToolStripMenuItem.Text = "One";
			this.oneToolStripMenuItem.Click += new System.EventHandler(this.oneToolStripMenuItem_Click);
			// 
			// allToolStripMenuItem
			// 
			this.allToolStripMenuItem.Name = "allToolStripMenuItem";
			this.allToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.allToolStripMenuItem.Text = "All";
			this.allToolStripMenuItem.Click += new System.EventHandler(this.allToolStripMenuItem_Click);
			// 
			// byIdToolStripMenuItem
			// 
			this.byIdToolStripMenuItem.Name = "byIdToolStripMenuItem";
			this.byIdToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.byIdToolStripMenuItem.Text = "By _id";
			this.byIdToolStripMenuItem.Click += new System.EventHandler(this.byIdToolStripMenuItem_Click);
			// 
			// countToolStripMenuItem
			// 
			this.countToolStripMenuItem.Name = "countToolStripMenuItem";
			this.countToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.countToolStripMenuItem.Text = "Count";
			this.countToolStripMenuItem.Click += new System.EventHandler(this.countToolStripMenuItem_Click);
			// 
			// limitTo50ToolStripMenuItem
			// 
			this.limitTo50ToolStripMenuItem.Name = "limitTo50ToolStripMenuItem";
			this.limitTo50ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.limitTo50ToolStripMenuItem.Text = "Limit to 50";
			this.limitTo50ToolStripMenuItem.Click += new System.EventHandler(this.limitTo50ToolStripMenuItem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(876, 616);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "Mongdio v0.2";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.ctxMenuStripDb.ResumeLayout(false);
			this.ctxMenuStripCol.ResumeLayout(false);
			this.ctxMenuStripRoot.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TreeView treeViewMenu;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem howToToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip ctxMenuStripDb;
		private System.Windows.Forms.ToolStripMenuItem dropToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem newQueryToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip ctxMenuStripCol;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
		private System.Windows.Forms.ContextMenuStrip ctxMenuStripRoot;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem oneToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem byIdToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem countToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem limitTo50ToolStripMenuItem;
	}
}

