namespace Lab6
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
            this.button1 = new System.Windows.Forms.Button();
            this.LabelComputeTip = new System.Windows.Forms.Label();
            this.LabelBill = new System.Windows.Forms.Label();
            this.TextBoxBill = new System.Windows.Forms.TextBox();
            this.TextBoxComputeTip = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(163, 270);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // LabelComputeTip
            // 
            this.LabelComputeTip.AutoSize = true;
            this.LabelComputeTip.Location = new System.Drawing.Point(163, 186);
            this.LabelComputeTip.Name = "LabelComputeTip";
            this.LabelComputeTip.Size = new System.Drawing.Size(134, 25);
            this.LabelComputeTip.TabIndex = 1;
            this.LabelComputeTip.Text = "Compute Tip";
            this.LabelComputeTip.Click += new System.EventHandler(this.LabelComputeTip_Click);
            // 
            // LabelBill
            // 
            this.LabelBill.AutoSize = true;
            this.LabelBill.Location = new System.Drawing.Point(168, 105);
            this.LabelBill.Name = "LabelBill";
            this.LabelBill.Size = new System.Drawing.Size(152, 25);
            this.LabelBill.TabIndex = 2;
            this.LabelBill.Text = "Enter Total Bill";
            this.LabelBill.Click += new System.EventHandler(this.LabelBill_Click);
            // 
            // TextBoxBill
            // 
            this.TextBoxBill.Location = new System.Drawing.Point(348, 105);
            this.TextBoxBill.Name = "TextBoxBill";
            this.TextBoxBill.Size = new System.Drawing.Size(100, 31);
            this.TextBoxBill.TabIndex = 3;
            this.TextBoxBill.TextChanged += new System.EventHandler(this.TextBoxBill_TextChanged);
            // 
            // TextBoxComputeTip
            // 
            this.TextBoxComputeTip.Location = new System.Drawing.Point(348, 179);
            this.TextBoxComputeTip.Name = "TextBoxComputeTip";
            this.TextBoxComputeTip.Size = new System.Drawing.Size(100, 31);
            this.TextBoxComputeTip.TabIndex = 4;
            this.TextBoxComputeTip.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TextBoxComputeTip);
            this.Controls.Add(this.TextBoxBill);
            this.Controls.Add(this.LabelBill);
            this.Controls.Add(this.LabelComputeTip);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label LabelComputeTip;
        private System.Windows.Forms.Label LabelBill;
        private System.Windows.Forms.TextBox TextBoxBill;
        private System.Windows.Forms.TextBox TextBoxComputeTip;
    }
}

