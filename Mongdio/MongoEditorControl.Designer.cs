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
			this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripColoring = new System.Windows.Forms.ToolStripButton();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.rtEditor = new System.Windows.Forms.RichTextBox();
			this.rtResult = new System.Windows.Forms.RichTextBox();
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
            this.toolStripComboBox1,
            this.toolStripColoring});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(583, 25);
			this.toolStrip1.TabIndex = 4;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripComboBox1
			// 
			this.toolStripComboBox1.Name = "toolStripComboBox1";
			this.toolStripComboBox1.Size = new System.Drawing.Size(121, 25);
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
			this.rtResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtResult.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rtResult.Location = new System.Drawing.Point(0, 0);
			this.rtResult.Name = "rtResult";
			this.rtResult.Size = new System.Drawing.Size(583, 233);
			this.rtResult.TabIndex = 0;
			this.rtResult.Text = "";
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
		private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.RichTextBox rtEditor;
		private System.Windows.Forms.RichTextBox rtResult;
		private System.Windows.Forms.ToolStripButton toolStripColoring;
	}
}
