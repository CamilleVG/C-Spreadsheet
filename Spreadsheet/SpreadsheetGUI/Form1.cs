// Written by Camille van Ginkel for PS6 assignment for CS 3500, October 2020

using SS;
using System;
using System.IO;
using System.Windows.Forms;

namespace SpreadsheetGUI
{
    /// <summary>
    /// This partial class deals with events on components
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            //creates Form1 object as defined in Form1.design.cs
            InitializeComponent();

            //adds displaySelection to listen for an occurence of a cell selection change
            spreadsheetPanel1.SelectionChanged += displaySelection;
            //the default cell selection when the window is opened is "C4"
            spreadsheetPanel1.SetSelection(2, 3);
        }

        //Deals with changes to the cell currently selcted in spreadsheet
        private void displaySelection(SpreadsheetPanel ss)
        {
            this.ActiveControl = spreadsheetPanel1;

            //Updates TextBoxs to contain the name, value, and contents of the selected cell
            CellNameTextBox.Text = "Cell: " + spreadsheetPanel1.GetCellName();
            ValueTextBox.Text = "Value: " + spreadsheetPanel1.GetCellValue();
            ContentsTextBox.Text = "Contents: " + spreadsheetPanel1.GetCellContents();
        }

        //Deals with click on New menu item
        private void NewMenuItem_Click_1(object sender, EventArgs e)
        {
            //Tell the application context to run the form on the same thread as the other forms.
            //Creats a new spreadsheet window
            SpreadsheetApplicationContext.getAppContext().RunForm(new Form1());
        }

        //Deals with click on Open menu item
        private void OpenMenuItem_Click(object sender, EventArgs e)
        {

            //if the user selects to No, the application will close
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (spreadsheetPanel1.IsChanged())
                {
                    // Display a MsgBox asking the user to save changes or abort.
                    if (MessageBox.Show("Opening a file before saving changes to your spreadsheet may lose data.  Do you want to save changes to your text?", "My Application",
                       MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        spreadsheetPanel1.Open(openFileDialog1.FileName);
                    }
                }
                
            }

        }

        //Deals with click on  Close menu item
        private void CloseMenuItem_Click(object sender, EventArgs e)
        {
            //Shows user warning that closing app could lose unsaved changes
            if (spreadsheetPanel1.IsChanged())
            {
                MessageBox.Show("Not all changes have been saved.  Please save file before closing");
            }
            else
            {
                Close();
            }

        }

        // When the Contents text box is clicked or double clicked.
        // The TextBox is set to the contents of the current cell and it loses the label "Contents: "
        private void ContentsTextBox_Clicked(object sender, EventArgs e)
        {
            ContentsTextBox.Text = spreadsheetPanel1.GetCellContents();
        }


        //Deals with the enter button being pressed after user has updated the contents of a cell
        private void ContentsTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    spreadsheetPanel1.SetContents(ContentsTextBox.Text);
                    ContentsTextBox.Text = "Contents: " + ContentsTextBox.Text;
                    CellNameTextBox.Text = "Cell: " + spreadsheetPanel1.GetCellName();
                    ValueTextBox.Text = "Value: " + spreadsheetPanel1.GetCellValue();

                    //the cursor will no longer blink in textbox
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

        //Deals with click on Save menu item
        private void SaveMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = saveFileDialog1.FileName;
                spreadsheetPanel1.Save(filename);
            }
        }
        
        //Deals with click on closing 'X'
        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            if (spreadsheetPanel1.IsChanged())
            {
                // Display a MsgBox asking the user to save changes or abort.
                if (MessageBox.Show("Do you want to save changes to your text?", "My Application",
                   MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Cancel the Closing event from closing the form.
                    e.Cancel = true;
                }
                //if the user selects to No, the application will close

            }

        }

        //Deals with click on Print menu item
        private void PrintMenuItem_Click(object sender, EventArgs e)
        {
            String filename = @"C:\Users\Camille\source\repos\spreadsheet-CamilleVG\Spreadsheet\SpreadsheetTests\save.txt";
            spreadsheetPanel1.Save(filename);
            printDocument1.DocumentName = "Print Document";
            printDialog1.Document = printDocument1;
            printDialog1.AllowSelection = true;
            printDialog1.AllowSomePages = true;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        //Deals with "How to open a spreadsheet?"
        private void howToCreateNewSpreadsheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To open a new spreadsheet, select the File menu.  In the File menu select New.  A new window containing an empty spreadsheet will automatically pop up.");
        }

        //Deals with "How to change contents of a cell?"
        private void howToChangeContentsOfCellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Use mouse to select a cell on grid.  Once the desired cell is selected, click on the contents textbox and enter " +
                "contents of a cell. If you would like to enter a formula remember to prepend '=' to the front of your formula. Once you have finished entering" +
                " a text, number, or formula into the cell, press enter to update the spreadsheet.");
        }
        //Deals with "How to input a formula?
        private void howToEnterAFormulaIntoACellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("In this spreadsheet you have the option to enter a formula into a cell and the spreadsheet will automatically evaluate the formula. " +
                "To input a formula, edit the contents of a cell by selecting a cell on the grid" +
                " and then selcting the contents text box.  Append '=' to the front of a standard infix expression.  You also have the option to input the names of other cells " +
                "in the grid as variables into a formula such that the formula computes operations on the values of other cells. " +
                "If a cell contains a formula that depends on the value of other cells" +
                " the spreadsheet will automatically update the value of that cell when one of its variables is changed.  Once you have finished inputing" +
                " the formula, press enter to update the spreadsheet.");
        }

        //Deals with "How to open a file?"
        private void howToOpenAPreexisitngSpreadsheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To open a spreadsheet from a pre-existing file in file directory, select the File menu.  In the File menu select Open. The contents of your spreadsheet in your current window will update to the contents of the selected file.");
        }

        private void howToCloseProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To close the program you have two options.  The first option is to click the 'X' in the upper right hand corner.  The second option is to click the File menu.  In the file menu select Close.  If you try to close the program before saving changes" +
                " a dialog box will pop up warning you that you may lose unsaved changes. ");
        }

        private void howToSaveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To save the your file, click on the File menu.  In the file menu select Save.  This will pop up a dialog box for saving a file.  The spreadsheet '.sprd' will be automatically added to the file name you enter.");
        }

        //Deals with user selecting to change the color of the grid
        private void changeGridColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                spreadsheetPanel1.GetColor(colorDialog1.Color);
                spreadsheetPanel1.SetSelection(2, 3);  //this makes the spreadsheet repaint to update the color
            }
            
        }

        //Deals with "How to change the color of the grid lines
        private void howToChangeTheColorOfTheGridLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To change the color of the gridlines, click on the File menu.  In the file menu click on tools," +
                " then click on Change Color of Grid. A menu of colors to select from will pop up.  Choose a color and click ok. The grid lines should appear as the color you selected.");
        }

        
    }
    }

