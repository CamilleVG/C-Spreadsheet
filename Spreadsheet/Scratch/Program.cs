using System;
using System.Text.RegularExpressions;
/// <summary>
/// Let's add a comment to my solution and practice swapping between the master branch
/// and the new PS5 branch.
/// </summary>

namespace Scratch
{
    class Program
    {
        static void Main(string[] args)
        {
            //Practicing Regex Expressions
            Console.WriteLine("Run Works!");

            String dollarPattern = "I saw ([0-9]|like, tons of) dogs today";
            String dollar = "I saw dogs today";

            string varpattern = @"^[A-Z]([1-9]|[1-9][1-9])$";//([1 - 9] | 1[0 - 9] | 2[0 - 5])
            string cell = "B66";


            if (Regex.IsMatch(cell, varpattern))
            {
                Console.WriteLine(cell + " matches");
            }

        }
    }
}







string varpattern = @"^[A-Z]([1-9]|[1-9][1-9])$";
sp = new Spreadsheet(s => Regex.IsMatch(s, varpattern), s => s, "1.00.00");
        }


        /// <summary>
        /// Clears the display.
        /// </summary>

        public void Clear()
{
    drawingPanel.Clear();
}


/// <summary>
/// If the zero-based column and row are in range, sets the value of that
/// cell and returns true.  Otherwise, returns false.
/// </summary>
/// <param name="col"></param>
/// <param name="row"></param>
/// <param name="value"></param>
/// <returns></returns>

public bool SetContents(int col, int row, string contents)
{
    string name = this.GetCellName(col, row);
    IList<string> dependents = sp.SetContentsOfCell(name, contents); //could throw circularArgument
    foreach (string cell in dependents)
    {
        object CellValue = sp.GetCellValue(cell);
        if (CellValue is FormulaError)
        {
            CellValue = "FormulaError";
        }
        int CellCol;
        int CellRow;
        GetCellRowAndCol(cell, out CellCol, out CellRow);
        drawingPanel.SetValue(CellCol, CellRow, CellValue.ToString());
    }
    object value = sp.GetCellValue(name);
    if (value is FormulaError)
    {
        value = "FormulaError";
    }
    return drawingPanel.SetValue(col, row, value.ToString());
}

private void GetCellRowAndCol(string name, out int col, out int row)
{
    char letter = name[0];
    double column = Convert.ToInt32(letter);
    column = column - 64;
    string num = name.Substring(1);
    col = int.Parse(column.ToString()) - 1;
    row = int.Parse(num) - 1;
}


/// <summary>
/// If the zero-based column and row are in range, assigns the value
/// of that cell to the out parameter and returns true.  Otherwise,
/// returns false.
/// </summary>
/// <param name="col"></param>
/// <param name="row"></param>
/// <param name="value"></param>
/// <returns></returns>

public bool GetValue(int col, int row, out string value)
{
    return drawingPanel.GetValue(col, row, out value);
}


/// <summary>
/// If the zero-based column and row are in range, uses them to set
/// the current selection and returns true.  Otherwise, returns false.
/// </summary>
/// <param name="col"></param>
/// <param name="row"></param>
/// <returns></returns>

public bool SetSelection(int col, int row)
{
    return drawingPanel.SetSelection(col, row);
}


/// <summary>
/// Assigns the column and row of the current selection to the
/// out parameters.
/// </summary>
/// <param name="col"></param>
/// <param name="row"></param>

public void GetSelection(out int col, out int row)
{
    drawingPanel.GetSelection(out col, out row);
}

/// <summary>
/// Gets the name of the cell that is currently selected in the spreadsheet panel
/// </summary>
/// <returns>The name of the current selected cell</returns>
public string GetCellName()
{
    int col, row;
    drawingPanel.GetSelection(out col, out row);
    col += 1;
    row += 1;
    int unicode = col + 64;
    string columnLetter = (Convert.ToChar(unicode)).ToString();
    String name = columnLetter + row;
    return name;
}

/// <summary>
/// Gets the name of any cell given the location it is in the grid of the spreadsheet panel.
/// The column number is converted to the letter that represents that column.  The cell name 
/// is returned as a string concatenating the column letter and row number.  
/// </summary>
/// <param name="col">Collumn of cell in grid</param>
/// <param name="row">Row of cell in grid</param>
/// <returns>Name of cell</returns>
public string GetCellName(int col, int row)
{
    col += 1;
    row += 1;
    int unicode = col + 64;
    string columnLetter = (Convert.ToChar(unicode)).ToString();
    String name = columnLetter + row;
    return name;
}

/// <summary>
/// 
/// </summary>
/// <returns></returns>
public Object GetCellValue()
{
    try
    {
        String name = GetCellName();
        object value = sp.GetCellValue(name);
        if (value is FormulaError)
        {
            value = "FormulaError";
        }
        return value;
    }
    catch
    {
        return "Error Occured in Panel";
    }

}
public string GetCellContents()
{
    try
    {
        String name = GetCellName();
        string contents = (string)sp.GetCellContents(name);
        return contents;
    }
    catch
    {
        return "Error Occured in Panel";
    }

}
