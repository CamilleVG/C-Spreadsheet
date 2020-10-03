///Written by Camille van Ginkel for PS5 assignment for CS 3500, October 2020

using SpreadsheetUtilities;
using System;

namespace SS
{
    /// <summary>
    /// A Cell object represents a non-empty cell in a spreadsheet and has two member variables: contents and value.  
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// <para>The input of the cell. </para> 
        /// It must be either a string, double, or Formula.
        /// It is private beacause contents of a cell should only be
        /// set by calling spreadsheets SetCellContents() method.
        /// </summary>
        private object contents;

        /// <summary>
        /// <para>The value of the input of a cell.</para>
        /// It must be either a string, double, or FormulaError.
        /// 
        /// If the cell's contents is a string, its value is that string.
        /// 
        /// If the cell's contents is a double, its value is that double.
        /// 
        /// If the cells contents is a formula, its value is the output of the evaluated formula.
        /// If the formula is evaluated and returns FormulaError, value is set to a FormulaError.  Otherwise, 
        /// the value of an input formula is a double.
        /// </summary>
        private object value;

        /// <summary>
        /// Instantiates a cell with string input.
        /// </summary>
        /// <param name="Contents">The String that was input into the cell.</param>
        public Cell(string Contents)
        {
            contents = Contents;
            value = Contents;
        }

        /// <summary>
        /// Instantiates a cell with type double input.
        /// </summary>
        /// <param name="Contents">The double number that was input into the cell.</param>
        public Cell(double Contents)
        {
            contents = Contents;
            value = Contents;
        }

        /// <summary>
        /// Instantiates a cell with type Formula input.
        /// </summary>
        /// <param name="Contents">The formula that was input into the cell.</param>
        /// <param name="Lookup"> Lookup method to return the value of a cell(variable) is defined in spreadsheet</param>
        public Cell(Formula Contents, Func<string, double> Lookup)
        {
            contents = Contents;
            value = Contents.Evaluate(Lookup);
        }

        

        /// <summary>
        /// Returns the input of a cell.
        /// </summary>
        public object Contents
        {
            get { return contents; }
        }

        /// <summary>
        /// Returns the input of a cell.
        /// </summary>
        public object Value
        {
            get { return value; }
        }

        /// <summary>
        /// Changes the contents of a cell to the given input.
        /// </summary>
        /// <param name="input">The input of a cell.  It must be a double, Formula, or String.</param>
        public void SetContents(object input)
        {
            
            contents = input;
            if (input is string || input is double)
            {
                value = input;
            }
            else
            {
                value = null;
            }
        }


    }
}
