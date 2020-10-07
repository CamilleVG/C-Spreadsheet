using SS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpreadsheetGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            spreadsheetPanel1.SelectionChanged += displaySelection;
            spreadsheetPanel1.SetSelection(2, 3);
        }

        private void displaySelection(SpreadsheetPanel ss)
        {
            int col, row;
            ss.GetSelection(out col, out row);
            string name = ss.GetCellName();
            CellNameTextBox.Text = "Cell: " + spreadsheetPanel1.GetCellName();
            ValueTextBox.Text = "Value: " + spreadsheetPanel1.GetCellValue();
            ContentsTextBox.Text = "Contents: " + spreadsheetPanel1.GetCellContents();
            ss.SetContents(col, row, "5+7");
            //highlight selected cell
        }


        private void NewMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void NewMenuItem_Click_1(object sender, EventArgs e)
        {
            //Create an empty spreadsheet in new window

        }

        private void OpenMenuItem_Click(object sender, EventArgs e)
        {
            //OpenFileDialogBox
        }

        private void CloseMenuItem_Click(object sender, EventArgs e)
        {
            ///close Window
        }


    }
}
