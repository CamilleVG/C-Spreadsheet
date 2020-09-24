///Written by Camille van Ginkel for PS4 assignment for CS 3500, September 2020

using SpreadsheetUtilities;

namespace SS
{
    /// <summary>
    /// A Cell object represents a non-empty cell in a spreades and has two member variables: contents and value.  
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// contents: is the input of the cell.  
        /// It must be either a string, double, or Formula.
        /// </summary>
        private object contents;

        /// <summary>
        /// value: is the value of the input of a cell.
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
            value = null; ///For now the value of a Formula is null
                          ///Need to define lookup method for formula.Evaluate()
        }
        public object Contents
        {
            get { return contents; }
        }

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
