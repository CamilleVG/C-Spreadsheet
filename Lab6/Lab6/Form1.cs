using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void LabelComputeTip_Click(object sender, EventArgs e)
        {
            double tip = Double.Parse(TextBoxBill.Text);
            tip = tip * .2;
            string percentTip = tip + "$";
            TextBoxComputeTip.Text = percentTip;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBoxBill_TextChanged(object sender, EventArgs e)
        {

        }

        private void LabelBill_Click(object sender, EventArgs e)
        {

        }
    }
}
