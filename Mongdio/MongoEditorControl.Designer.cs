namespace Mongdio
{
	partial class MongoEditorControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MongoEditorControl));
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripCommandLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripColoring = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonRun = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.rtEditor = new System.Windows.Forms.RichTextBox();
			this.rtResult = new System.Windows.Forms.RichTextBox();
			this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
			this.statusStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripCommandLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 511);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(583, 22);
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripCommandLabel
			// 
			this.toolStripCommandLabel.Name = "toolStripCommandLabel";
			this.toolStripCommandLabel.Size = new System.Drawing.Size(20, 17);
			this.toolStripCommandLabel.Text = "Ok";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripColoring,
            this.toolStripButtonRun,
            this.toolStripButtonSave,
            this.toolStripButtonDelete});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(583, 25);
			this.toolStrip1.TabIndex = 4;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripColoring
			// 
			this.toolStripColoring.CheckOnClick = true;
			this.toolStripColoring.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripColoring.Image = ((System.Drawing.Image)(resources.GetObject("toolStripColoring.Image")));
			this.toolStripColoring.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripColoring.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripColoring.Name = "toolStripColoring";
			this.toolStripColoring.Size = new System.Drawing.Size(50, 22);
			this.toolStripColoring.Text = "Coloring";
			// 
			// toolStripButtonRun
			// 
			this.toolStripButtonRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButtonRun.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRun.Image")));
			this.toolStripButtonRun.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonRun.Name = "toolStripButtonRun";
			this.toolStripButtonRun.Size = new System.Drawing.Size(73, 22);
			this.toolStripButtonRun.Text = "Execute (F5)";
			this.toolStripButtonRun.Click += new System.EventHandler(this.toolStripButtonRun_Click);
			// 
			// toolStripButtonSave
			// 
			this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButtonSave.Enabled = false;
			this.toolStripButtonSave.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSave.Image")));
			this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonSave.Name = "toolStripButtonSave";
			this.toolStripButtonSave.Size = new System.Drawing.Size(91, 22);
			this.toolStripButtonSave.Text = "Save object (F6)";
			this.toolStripButtonSave.Click += new System.EventHandler(this.toolStripButtonSave_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 25);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.rtEditor);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.rtResult);
			this.splitContainer1.Size = new System.Drawing.Size(583, 486);
			this.splitContainer1.SplitterDistance = 249;
			this.splitContainer1.TabIndex = 5;
			// 
			// rtEditor
			// 
			this.rtEditor.AcceptsTab = true;
			this.rtEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtEditor.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rtEditor.Location = new System.Drawing.Point(0, 0);
			this.rtEditor.Name = "rtEditor";
			this.rtEditor.Size = new System.Drawing.Size(583, 249);
			this.rtEditor.TabIndex = 0;
			this.rtEditor.Text = "";
			// 
			// rtResult
			// 
			this.rtResult.AcceptsTab = true;
			this.rtResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtResult.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rtResult.Location = new System.Drawing.Point(0, 0);
			this.rtResult.Name = "rtResult";
			this.rtResult.Size = new System.Drawing.Size(583, 233);
			this.rtResult.TabIndex = 0;
			this.rtResult.Text = "";
			this.rtResult.SelectionChanged += new System.EventHandler(this.rtResult_SelectionChanged);
			// 
			// toolStripButtonDelete
			// 
			this.toolStripButtonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButtonDelete.Enabled = false;
			this.toolStripButtonDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDelete.Image")));
			this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonDelete.Name = "toolStripButtonDelete";
			this.toolStripButtonDelete.Size = new System.Drawing.Size(71, 22);
			this.toolStripButtonDelete.Text = "Delete (F12)";
			this.toolStripButtonDelete.Click += new System.EventHandler(this.toolStripButtonDelete_Click);
			// 
			// MongoEditorControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.statusStrip1);
			this.Name = "MongoEditorControl";
			this.Size = new System.Drawing.Size(583, 533);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripCommandLabel;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.RichTextBox rtEditor;
		private System.Windows.Forms.RichTextBox rtResult;
		private System.Windows.Forms.ToolStripButton toolStripColoring;
		private System.Windows.Forms.ToolStripButton toolStripButtonRun;
		private System.Windows.Forms.ToolStripButton toolStripButtonSave;
		private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
	}
}
