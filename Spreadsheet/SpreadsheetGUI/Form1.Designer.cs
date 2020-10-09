
using System.Windows.Forms;


namespace SpreadsheetGUI
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.option1MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.option2MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LowerContentsPanel = new System.Windows.Forms.Panel();
            this.ValueTextBox = new System.Windows.Forms.TextBox();
            this.ContentsTextBox = new System.Windows.Forms.TextBox();
            this.CellNameTextBox = new System.Windows.Forms.TextBox();
            this.spreadsheetPanel1 = new SS.SpreadsheetPanel();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.menuStrip1.SuspendLayout();
            this.LowerContentsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1022, 40);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewMenuItem,
            this.OpenMenuItem,
            this.CloseMenuItem,
            this.SettingsMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(72, 36);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // NewMenuItem
            // 
            this.NewMenuItem.Name = "NewMenuItem";
            this.NewMenuItem.Size = new System.Drawing.Size(235, 44);
            this.NewMenuItem.Text = "New";
            this.NewMenuItem.Click += new System.EventHandler(this.NewMenuItem_Click_1);
            // 
            // OpenMenuItem
            // 
            this.OpenMenuItem.Name = "OpenMenuItem";
            this.OpenMenuItem.Size = new System.Drawing.Size(235, 44);
            this.OpenMenuItem.Text = "Open";
            this.OpenMenuItem.Click += new System.EventHandler(this.OpenMenuItem_Click);
            // 
            // CloseMenuItem
            // 
            this.CloseMenuItem.Name = "CloseMenuItem";
            this.CloseMenuItem.Size = new System.Drawing.Size(235, 44);
            this.CloseMenuItem.Text = "Close";
            this.CloseMenuItem.Click += new System.EventHandler(this.CloseMenuItem_Click);
            // 
            // SettingsMenuItem
            // 
            this.SettingsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.option1MenuItem,
            this.option2MenuItem});
            this.SettingsMenuItem.Name = "SettingsMenuItem";
            this.SettingsMenuItem.Size = new System.Drawing.Size(235, 44);
            this.SettingsMenuItem.Text = "Settings";
            // 
            // option1MenuItem
            // 
            this.option1MenuItem.Name = "option1MenuItem";
            this.option1MenuItem.Size = new System.Drawing.Size(236, 44);
            this.option1MenuItem.Text = "Option1";
            // 
            // option2MenuItem
            // 
            this.option2MenuItem.Name = "option2MenuItem";
            this.option2MenuItem.Size = new System.Drawing.Size(236, 44);
            this.option2MenuItem.Text = "Option2";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(85, 36);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // LowerContentsPanel
            // 
            this.LowerContentsPanel.Controls.Add(this.vScrollBar1);
            this.LowerContentsPanel.Controls.Add(this.ValueTextBox);
            this.LowerContentsPanel.Controls.Add(this.ContentsTextBox);
            this.LowerContentsPanel.Controls.Add(this.CellNameTextBox);
            this.LowerContentsPanel.Controls.Add(this.spreadsheetPanel1);
            this.LowerContentsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LowerContentsPanel.Location = new System.Drawing.Point(0, 40);
            this.LowerContentsPanel.Name = "LowerContentsPanel";
            this.LowerContentsPanel.Size = new System.Drawing.Size(1022, 514);
            this.LowerContentsPanel.TabIndex = 3;
            // 
            // ValueTextBox
            // 
            this.ValueTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ValueTextBox.Enabled = false;
            this.ValueTextBox.Location = new System.Drawing.Point(521, 75);
            this.ValueTextBox.Name = "ValueTextBox";
            this.ValueTextBox.Size = new System.Drawing.Size(236, 31);
            this.ValueTextBox.TabIndex = 4;
            this.ValueTextBox.Text = "Value: ";
            // 
            // ContentsTextBox
            // 
            this.ContentsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ContentsTextBox.Location = new System.Drawing.Point(283, 75);
            this.ContentsTextBox.Name = "ContentsTextBox";
            this.ContentsTextBox.Size = new System.Drawing.Size(232, 31);
            this.ContentsTextBox.TabIndex = 3;
            this.ContentsTextBox.Text = "Contents: ";
            this.ContentsTextBox.Click += new System.EventHandler(this.ContentsTextBox_Clicked);
            this.ContentsTextBox.DoubleClick += new System.EventHandler(this.ContentsTextBox_Clicked);
            this.ContentsTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ContentsTextBox_KeyDown);
            // 
            // CellNameTextBox
            // 
            this.CellNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CellNameTextBox.Enabled = false;
            this.CellNameTextBox.Location = new System.Drawing.Point(7, 75);
            this.CellNameTextBox.Name = "CellNameTextBox";
            this.CellNameTextBox.Size = new System.Drawing.Size(271, 31);
            this.CellNameTextBox.TabIndex = 1;
            this.CellNameTextBox.Text = "Cell: C4";
            // 
            // spreadsheetPanel1
            // 
            this.spreadsheetPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.spreadsheetPanel1.AutoSize = true;
            this.spreadsheetPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.spreadsheetPanel1.Location = new System.Drawing.Point(0, 119);
            this.spreadsheetPanel1.Name = "spreadsheetPanel1";
            this.spreadsheetPanel1.Size = new System.Drawing.Size(1022, 395);
            this.spreadsheetPanel1.TabIndex = 0;
            this.spreadsheetPanel1.Load += new System.EventHandler(this.spreadsheetPanel1_Load);
            this.spreadsheetPanel1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.spreadsheetPanel1_KeyDown);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(812, 33);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(34, 160);
            this.vScrollBar1.TabIndex = 5;
            this.vScrollBar1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.spreadsheetPanel1_KeyDown);
            // 
            // Form1
            // 
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1022, 554);
            this.Controls.Add(this.LowerContentsPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1048, 625);
            this.Name = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.LowerContentsPanel.ResumeLayout(false);
            this.LowerContentsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SS.SpreadsheetPanel spreadsheetPanel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CloseMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SettingsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem option1MenuItem;
        private System.Windows.Forms.ToolStripMenuItem option2MenuItem;
        private System.Windows.Forms.Panel LowerContentsPanel;
        private System.Windows.Forms.TextBox ValueTextBox;
        private System.Windows.Forms.TextBox ContentsTextBox;
        private System.Windows.Forms.TextBox CellNameTextBox;
        private VScrollBar vScrollBar1;
    }
}

