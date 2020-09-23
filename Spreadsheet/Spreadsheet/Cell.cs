using System;
using System.Collections.Generic;
using System.Text;
using SpreadsheetUtilities;

namespace SS
{
    /// <summary>
    /// An AbstractSpreadsheet object represents the state of a simple spreadsheet.  A 
    /// spreadsheet consists of an infinite number of named cells.
    /// 
    /// A string is a valid cell name if and only if:
    ///   (1) its first character is an underscore or a letter
    ///   (2) its remaining characters (if any) are underscores and/or letters and/or digits
    /// Note that this is the same as the definition of valid variable from the PS3 Formula class.
    /// 
    /// For example, "x", "_", "x2", "y_15", and "___" are all valid cell  names, but
    /// "25", "2x", and "&" are not.  Cell names are case sensitive, so "x" and "X" are
    /// different cell names.
    /// 
    /// A spreadsheet contains a cell corresponding to every possible cell name.  (This
    /// means that a spreadsheet contains an infinite number of cells.)  In addition to 
    /// a name, each cell has a contents and a value.  The distinction is important.
    /// 
    /// The contents of a cell can be (1) a string, (2) a double, or (3) a Formula.  If the
    /// contents is an empty string, we say that the cell is empty.  (By analogy, the contents
    /// of a cell in Excel is what is displayed on the editing line when the cell is selected.)
    /// 
    /// In a new spreadsheet, the contents of every cell is the empty string.
    ///  
    /// The value of a cell can be (1) a string, (2) a double, or (3) a FormulaError.  
    /// (By analogy, the value of an Excel cell is what is displayed in that cell's position
    /// in the grid.)
    /// 
    /// If a cell's contents is a string, its value is that string.
    /// 
    /// If a cell's contents is a double, its value is that double.
    /// 
    /// If a cell's contents is a Formula, its value is either a double or a FormulaError,
    /// as reported by the Evaluate method of the Formula class.  The value of a Formula,
    /// of course, can depend on the values of variables.  The value of a variable is the 
    /// value of the spreadsheet cell it names (if that cell's value is a double) or 
    /// is undefined (otherwise).
    /// 
    /// Spreadsheets are never allowed to contain a combination of Formulas that establish
    /// a circular dependency.  A circular dependency exists when a cell depends on itself.
    /// For example, suppose that A1 contains B1*2, B1 contains C1*2, and C1 contains A1*2.
    /// A1 depends on B1, which depends on C1, which depends on A1.  That's a circular
    /// dependency.
    /// </summary>
    public class Cell
    {
        //private string name;
        private object contents; //string, double, or Formula
        private object value; //string, double, or FormulaError
        //type


        public Cell(string Contents)
        {
            contents = Contents;
            value = Contents;
        }
        public Cell(double Contents)
        {
            contents = Contents;
            value = Contents;

        }
        public Cell(Formula Contents)
        {
            contents = Contents;
            //for now the value of a formula is null;
            value = null;
            //Func<string, double> lookup = Spreadsheet.
            //need lookup method to evaluate formula
            //lookup method return the values of all of the variables
            //regursion =>  get dependees => get dependees of dependees 
            //=> until cell does not have and dependees
            //then return base cells value to dependents, the evaluate dependents with returned value and return value;
            //Func<string, double> lookup = name => CalculateValue(name);
            //Contents.Evaluate(name => CalculateValue(name)); //either a double or a formula error
        }
        public object Contents
        {
            get { return contents; }
        }
    }
}
