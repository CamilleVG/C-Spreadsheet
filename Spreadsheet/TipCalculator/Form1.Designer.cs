namespace TipCalculator
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
            this.LabelTip = new System.Windows.Forms.Label();
            this.labelBill = new System.Windows.Forms.Label();
            this.TextBoxTotalBill = new System.Windows.Forms.TextBox();
            this.TextBoxComputeTip = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LabelTip
            // 
            this.LabelTip.AutoSize = true;
            this.LabelTip.Location = new System.Drawing.Point(147, 197);
            this.LabelTip.Name = "LabelTip";
            this.LabelTip.Size = new System.Drawing.Size(134, 25);
            this.LabelTip.TabIndex = 0;
            this.LabelTip.Text = "Compute Tip";
            this.LabelTip.Click += new System.EventHandler(this.label1_Click);
            // 
            // labelBill
            // 
            this.labelBill.AutoSize = true;
            this.labelBill.Location = new System.Drawing.Point(147, 87);
            this.labelBill.Name = "labelBill";
            this.labelBill.Size = new System.Drawing.Size(152, 25);
            this.labelBill.TabIndex = 1;
            this.labelBill.Text = "Enter Total Bill";
            this.labelBill.Click += new System.EventHandler(this.label2_Click);
            // 
            // TextBoxTotalBill
            // 
            this.TextBoxTotalBill.Location = new System.Drawing.Point(398, 87);
            this.TextBoxTotalBill.Name = "TextBoxTotalBill";
            this.TextBoxTotalBill.Size = new System.Drawing.Size(100, 31);
            this.TextBoxTotalBill.TabIndex = 2;
            this.TextBoxTotalBill.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // TextBoxComputeTip
            // 
            this.TextBoxComputeTip.Location = new System.Drawing.Point(398, 197);
            this.TextBoxComputeTip.Name = "TextBoxComputeTip";
            this.TextBoxComputeTip.Size = new System.Drawing.Size(100, 31);
            this.TextBoxComputeTip.TabIndex = 3;
            this.TextBoxComputeTip.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(267, 401);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 25);
            this.label3.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(193, 219);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 25);
            this.label4.TabIndex = 5;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(152, 303);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.EnabledChanged += new System.EventHandler(this.button1_EnabledChanged);
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TextBoxComputeTip);
            this.Controls.Add(this.TextBoxTotalBill);
            this.Controls.Add(this.labelBill);
            this.Controls.Add(this.LabelTip);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelTip;
        private System.Windows.Forms.Label labelBill;
        private System.Windows.Forms.TextBox TextBoxTotalBill;
        private System.Windows.Forms.TextBox TextBoxComputeTip;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
    }
}

