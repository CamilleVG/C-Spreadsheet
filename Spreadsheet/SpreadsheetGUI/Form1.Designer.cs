
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
            this.SaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeGridColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PrintMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.howToChangeContentsOfCellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.howToEnterAFormulaIntoACellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.howToCreateNewSpreadsheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.howToOpenAPreexisitngSpreadsheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.howToCloseProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.howToSaveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.howToChangeTheColorOfTheGridLinesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LowerContentsPanel = new System.Windows.Forms.Panel();
            this.ValueTextBox = new System.Windows.Forms.TextBox();
            this.ContentsTextBox = new System.Windows.Forms.TextBox();
            this.CellNameTextBox = new System.Windows.Forms.TextBox();
            this.spreadsheetPanel1 = new SS.SpreadsheetPanel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
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
            this.menuStrip1.Size = new System.Drawing.Size(2000, 40);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewMenuItem,
            this.SaveMenuItem,
            this.OpenMenuItem,
            this.CloseMenuItem,
            this.SettingsMenuItem,
            this.PrintMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(72, 36);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // NewMenuItem
            // 
            this.NewMenuItem.Name = "NewMenuItem";
            this.NewMenuItem.Size = new System.Drawing.Size(208, 44);
            this.NewMenuItem.Text = "New";
            this.NewMenuItem.Click += new System.EventHandler(this.NewMenuItem_Click_1);
            // 
            // SaveMenuItem
            // 
            this.SaveMenuItem.Name = "SaveMenuItem";
            this.SaveMenuItem.Size = new System.Drawing.Size(208, 44);
            this.SaveMenuItem.Text = "Save";
            this.SaveMenuItem.Click += new System.EventHandler(this.SaveMenuItem_Click);
            // 
            // OpenMenuItem
            // 
            this.OpenMenuItem.Name = "OpenMenuItem";
            this.OpenMenuItem.Size = new System.Drawing.Size(208, 44);
            this.OpenMenuItem.Text = "Open";
            this.OpenMenuItem.Click += new System.EventHandler(this.OpenMenuItem_Click);
            // 
            // CloseMenuItem
            // 
            this.CloseMenuItem.Name = "CloseMenuItem";
            this.CloseMenuItem.Size = new System.Drawing.Size(208, 44);
            this.CloseMenuItem.Text = "Close";
            this.CloseMenuItem.Click += new System.EventHandler(this.CloseMenuItem_Click);
            // 
            // SettingsMenuItem
            // 
            this.SettingsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeGridColorToolStripMenuItem});
            this.SettingsMenuItem.Name = "SettingsMenuItem";
            this.SettingsMenuItem.Size = new System.Drawing.Size(208, 44);
            this.SettingsMenuItem.Text = "Tools";
            // 
            // changeGridColorToolStripMenuItem
            // 
            this.changeGridColorToolStripMenuItem.Name = "changeGridColorToolStripMenuItem";
            this.changeGridColorToolStripMenuItem.Size = new System.Drawing.Size(346, 44);
            this.changeGridColorToolStripMenuItem.Text = "Change Grid Color";
            this.changeGridColorToolStripMenuItem.Click += new System.EventHandler(this.changeGridColorToolStripMenuItem_Click);
            // 
            // PrintMenuItem
            // 
            this.PrintMenuItem.Name = "PrintMenuItem";
            this.PrintMenuItem.Size = new System.Drawing.Size(208, 44);
            this.PrintMenuItem.Text = "Print";
            this.PrintMenuItem.Click += new System.EventHandler(this.PrintMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.howToChangeContentsOfCellToolStripMenuItem,
            this.howToEnterAFormulaIntoACellToolStripMenuItem,
            this.howToCreateNewSpreadsheetToolStripMenuItem,
            this.howToOpenAPreexisitngSpreadsheetToolStripMenuItem,
            this.howToCloseProgramToolStripMenuItem,
            this.howToSaveFileToolStripMenuItem,
            this.howToChangeTheColorOfTheGridLinesToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(85, 36);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // howToChangeContentsOfCellToolStripMenuItem
            // 
            this.howToChangeContentsOfCellToolStripMenuItem.Name = "howToChangeContentsOfCellToolStripMenuItem";
            this.howToChangeContentsOfCellToolStripMenuItem.Size = new System.Drawing.Size(600, 44);
            this.howToChangeContentsOfCellToolStripMenuItem.Text = "How to change contents of cell?";
            this.howToChangeContentsOfCellToolStripMenuItem.Click += new System.EventHandler(this.howToChangeContentsOfCellToolStripMenuItem_Click);
            // 
            // howToEnterAFormulaIntoACellToolStripMenuItem
            // 
            this.howToEnterAFormulaIntoACellToolStripMenuItem.Name = "howToEnterAFormulaIntoACellToolStripMenuItem";
            this.howToEnterAFormulaIntoACellToolStripMenuItem.Size = new System.Drawing.Size(600, 44);
            this.howToEnterAFormulaIntoACellToolStripMenuItem.Text = "How to enter a formula into a cell?";
            this.howToEnterAFormulaIntoACellToolStripMenuItem.Click += new System.EventHandler(this.howToEnterAFormulaIntoACellToolStripMenuItem_Click);
            // 
            // howToCreateNewSpreadsheetToolStripMenuItem
            // 
            this.howToCreateNewSpreadsheetToolStripMenuItem.Name = "howToCreateNewSpreadsheetToolStripMenuItem";
            this.howToCreateNewSpreadsheetToolStripMenuItem.Size = new System.Drawing.Size(600, 44);
            this.howToCreateNewSpreadsheetToolStripMenuItem.Text = "How to create new spreadsheet?";
            this.howToCreateNewSpreadsheetToolStripMenuItem.Click += new System.EventHandler(this.howToCreateNewSpreadsheetToolStripMenuItem_Click);
            // 
            // howToOpenAPreexisitngSpreadsheetToolStripMenuItem
            // 
            this.howToOpenAPreexisitngSpreadsheetToolStripMenuItem.Name = "howToOpenAPreexisitngSpreadsheetToolStripMenuItem";
            this.howToOpenAPreexisitngSpreadsheetToolStripMenuItem.Size = new System.Drawing.Size(600, 44);
            this.howToOpenAPreexisitngSpreadsheetToolStripMenuItem.Text = "How to open a pre-exisitng spreadsheet?";
            this.howToOpenAPreexisitngSpreadsheetToolStripMenuItem.Click += new System.EventHandler(this.howToOpenAPreexisitngSpreadsheetToolStripMenuItem_Click);
            // 
            // howToCloseProgramToolStripMenuItem
            // 
            this.howToCloseProgramToolStripMenuItem.Name = "howToCloseProgramToolStripMenuItem";
            this.howToCloseProgramToolStripMenuItem.Size = new System.Drawing.Size(600, 44);
            this.howToCloseProgramToolStripMenuItem.Text = "How to close program?";
            this.howToCloseProgramToolStripMenuItem.Click += new System.EventHandler(this.howToCloseProgramToolStripMenuItem_Click);
            // 
            // howToSaveFileToolStripMenuItem
            // 
            this.howToSaveFileToolStripMenuItem.Name = "howToSaveFileToolStripMenuItem";
            this.howToSaveFileToolStripMenuItem.Size = new System.Drawing.Size(600, 44);
            this.howToSaveFileToolStripMenuItem.Text = "How to save file?";
            this.howToSaveFileToolStripMenuItem.Click += new System.EventHandler(this.howToSaveFileToolStripMenuItem_Click);
            // 
            // howToChangeTheColorOfTheGridLinesToolStripMenuItem
            // 
            this.howToChangeTheColorOfTheGridLinesToolStripMenuItem.Name = "howToChangeTheColorOfTheGridLinesToolStripMenuItem";
            this.howToChangeTheColorOfTheGridLinesToolStripMenuItem.Size = new System.Drawing.Size(600, 44);
            this.howToChangeTheColorOfTheGridLinesToolStripMenuItem.Text = "How to change the color of the grid lines?";
            this.howToChangeTheColorOfTheGridLinesToolStripMenuItem.Click += new System.EventHandler(this.howToChangeTheColorOfTheGridLinesToolStripMenuItem_Click);
            // 
            // LowerContentsPanel
            // 
            this.LowerContentsPanel.Controls.Add(this.ValueTextBox);
            this.LowerContentsPanel.Controls.Add(this.ContentsTextBox);
            this.LowerContentsPanel.Controls.Add(this.CellNameTextBox);
            this.LowerContentsPanel.Location = new System.Drawing.Point(0, 43);
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
            this.spreadsheetPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.spreadsheetPanel1.Location = new System.Drawing.Point(0, 1059);
            this.spreadsheetPanel1.Margin = new System.Windows.Forms.Padding(6);
            this.spreadsheetPanel1.Name = "spreadsheetPanel1";
            this.spreadsheetPanel1.Size = new System.Drawing.Size(2000, 700);
            this.spreadsheetPanel1.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Spreadsheet files (*.sprd)|*.sprd|All files (*.*)|*.*";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "sprd";
            this.saveFileDialog1.Filter = "Spreadsheet files (*.sprd)|*.sprd|All files (*.*)|*.*";
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // printDocument1
            // 
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2000, 1759);
            this.Controls.Add(this.spreadsheetPanel1);
            this.Controls.Add(this.LowerContentsPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_Closing);
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
        private System.Windows.Forms.Panel LowerContentsPanel;
        private System.Windows.Forms.TextBox ValueTextBox;
        private System.Windows.Forms.TextBox ContentsTextBox;
        private System.Windows.Forms.TextBox CellNameTextBox;
        private OpenFileDialog openFileDialog1;
        private ToolStripMenuItem SaveMenuItem;
        private SaveFileDialog saveFileDialog1;
        private ToolStripMenuItem PrintMenuItem;
        private PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private ColorDialog colorDialog1;
        private ToolStripMenuItem howToChangeContentsOfCellToolStripMenuItem;
        private ToolStripMenuItem howToSaveFileToolStripMenuItem;
        private ToolStripMenuItem howToCreateNewSpreadsheetToolStripMenuItem;
        private ToolStripMenuItem howToOpenAPreexisitngSpreadsheetToolStripMenuItem;
        private ToolStripMenuItem howToCloseProgramToolStripMenuItem;
        private ToolStripMenuItem howToEnterAFormulaIntoACellToolStripMenuItem;
        private ToolStripMenuItem changeGridColorToolStripMenuItem;
        private ToolStripMenuItem howToChangeTheColorOfTheGridLinesToolStripMenuItem;
    }
}

