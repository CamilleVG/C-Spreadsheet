// Written by Camille van Ginkel for PS6 assignment for CS 3500, October 2020


using SS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            this.ActiveControl = spreadsheetPanel1;
            int col, row;
            ss.GetSelection(out col, out row);
            string name = ss.GetCellName();
            CellNameTextBox.Text = "Cell: " + spreadsheetPanel1.GetCellName();
            ValueTextBox.Text = "Value: " + spreadsheetPanel1.GetCellValue();
            ContentsTextBox.Text = "Contents: " + spreadsheetPanel1.GetCellContents();
        }
        

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //Deals with New menu
        private void NewMenuItem_Click_1(object sender, EventArgs e)
        {
            //Tell the application context to run the form on the same
            // thread as the other forms.
            //Application.getApp
            //SpreadsheetApplicationContext.getAppContext().RunForm(new Form1());
        }

        // Deals with the Close menu
        private void OpenMenuItem_Click(object sender, EventArgs e)
        {
            //OpenFileDialogBox
        }

        private void CloseMenuItem_Click(object sender, EventArgs e)
        {
            //close Window
            Close();
        }

        /// <summary>
        /// When the Contents text box is clicked or double clicked.
        /// The TextBox is set to empty.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContentsTextBox_Clicked(object sender, EventArgs e)
        {
            ContentsTextBox.Text = spreadsheetPanel1.GetCellContents();
        }
        private void spreadsheetPanel1_KeyDown(object sender, KeyEventArgs e)
        {
            //int col, row;
            //spreadsheetPanel1.GetSelection(out col, out row);
            //if (e.KeyCode == Keys.Left  && (col > 1))
            //{
            //    col--;
            //    spreadsheetPanel1.SetSelection(col, row);
            //}
            //if (e.KeyCode == Keys.Right && (col < 26))
            //{
            //    col++;
            //    spreadsheetPanel1.SetSelection(col, row);
            //}
            //if (e.KeyCode == Keys.Up && (row > 1))
            //{
            //    row--;
            //    spreadsheetPanel1.SetSelection(col, row);
            //}
            //if (e.KeyCode == Keys.Down && (row < 99))
            //{
            //    row++;
            //    spreadsheetPanel1.SetSelection(col, row);
            //}
        }

            private void ContentsTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    int col, row;
                    spreadsheetPanel1.GetSelection(out col, out row);
                    spreadsheetPanel1.SetContents(col, row, ContentsTextBox.Text);
                    ContentsTextBox.Text = "Contents: " + ContentsTextBox.Text;
                    CellNameTextBox.Text = "Cell: " + spreadsheetPanel1.GetCellName();
                    ValueTextBox.Text = "Value: " + spreadsheetPanel1.GetCellValue();
                    //ProcessCmdKey(ref Message msg, Keys keyData);

                    //make it so that the cursor no longer is in textbox
                    this.ActiveControl = spreadsheetPanel1;
                }
                catch (CircularException)
                {
                    MessageBox.Show("You tried to set the contents of a cell to a fomula dependent on itself.");
                }
                catch
                {
                    MessageBox.Show("The formula entered was invalid");
                }
                
            }
            
        }

        private void spreadsheetPanel1_Load(object sender, EventArgs e)
        {

        }

        
    }
}
