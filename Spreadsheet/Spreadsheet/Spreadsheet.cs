
using SpreadsheetUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

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

    public class Spreadsheet : AbstractSpreadsheet
    {
        private DependencyGraph dg;
        private Dictionary<string, Cell> spreadsheet = new Dictionary<string, Cell>();

        private static Func<string, bool> IsValidName;
        
        /// <summary>
        /// Constructer creates an empty spreadsheet
        /// </summary>
        public Spreadsheet()
        {
            dg = new DependencyGraph();

            String varPattern = @"[a-zA-Z_](?: [a-zA-Z_]|\d)*";
            IsValidName = x => Regex.IsMatch(x, varPattern);
    }
        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, returns the contents (as opposed to the value) of the named cell.  The return
        /// value should be either a string, a double, or a Formula.
        public override object GetCellContents(string name)
        {
            if (name is null || IsValidName(name))
            {
                throw new NotImplementedException();
            }
            return spreadsheet[name].Contents;
        }
        /// <summary>
        /// Enumerates the names of all the non-empty cells in the spreadsheet.
        /// </summary>
        public override IEnumerable<string> GetNamesOfAllNonemptyCells()
        {
            if (spreadsheet.Count > 0)
            {
                foreach (string key in spreadsheet.Keys)
                {
                    yield return key;
                }
            }
        }
        
        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes number.  The method returns a
        /// list consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// list {A1, B1, C1} is returned.
        /// </summary>
        public override IList<string> SetCellContents(string name, double number)
        {
            if (name is null || !IsValidName(name))
            {
                throw new InvalidNameException();
            }
            try
            {
                Cell cell = new Cell(number);
                spreadsheet.Add(name, cell);
                IList<string> Dependents = new List<string>();
                foreach (string dependent in dg.GetDependents(name))
                {
                    Dependents.Add(dependent);
                }
                return Dependents;
            }
            catch
            {
                throw new NotImplementedException();
            }
                
        }
        /// <summary>
        /// If text is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes text.  The method returns a
        /// list consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// list {A1, B1, C1} is returned.
        /// </summary>
        public override IList<string> SetCellContents(string name, string text)
        {
            if (text is null)
            {
                throw new ArgumentNullException();
            }
            if (name is null || !IsValidName(name))
            {
                throw new InvalidNameException();
            }
            try
            {
                Cell cell = new Cell(text);
                spreadsheet.Add(name, cell);
                IList<string> Dependents = new List<string>();
                foreach (string dependent in dg.GetDependents(name))
                {
                    Dependents.Add(dependent);
                }
                return Dependents;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }
        /// <summary>
        /// If the formula parameter is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, if changing the contents of the named cell to be the formula would cause a 
        /// circular dependency, throws a CircularException, and no change is made to the spreadsheet.
        /// 
        /// Otherwise, the contents of the named cell becomes formula.  The method returns a
        /// list consisting of name plus the names of all other cells whose value depends,
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// list {A1, B1, C1} is returned.
        public override IList<string> SetCellContents(string name, Formula formula)
        {
            if (formula is null)
            {
                throw new ArgumentNullException();
            }
            if (name is null || !IsValidName(name))
            {
                throw new InvalidNameException();
            }
            try
            {
                GetCellsToRecalculate(name); //might need a try/catch statement
            }
            catch (CircularException)
            {
                throw new CircularException();
            }
            try
            {
                Cell cell = new Cell(formula);
                spreadsheet.Add(name, cell);
                foreach (string dependee in formula.GetVariables())
                {
                    dg.AddDependency(dependee, name);
                }
                IList<string> Dependents = new List<string>();
                foreach (string dependent in dg.GetDependents(name))
                {
                    Dependents.Add(dependent);
                }
                return Dependents;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }
        /// <summary>
        /// Returns an enumeration, without duplicates, of the names of all cells whose
        /// values depend directly on the value of the named cell.  In other words, returns
        /// an enumeration, without duplicates, of the names of all cells that contain
        /// formulas containing name.
        /// 
        /// For example, suppose that
        /// A1 contains 3
        /// B1 contains the formula A1 * A1
        /// C1 contains the formula B1 + A1
        /// D1 contains the formula B1 - C1
        /// The direct dependents of A1 are B1 and C1
        /// </summary>
        protected override IEnumerable<string> GetDirectDependents(string name)
        {
            if (!dg.HasDependents(name))
            {
                yield break;
            }
            else
            {
                foreach (String n in dg.GetDependents(name))
                {
                    yield return n;
                }
            }
            
        }


        /// <summary>
        /// Given "x1" 
        ///     If the cell "x1" is a double returns value
        ///     If the cell "x1" is a string throws ArgumentException
        ///     If the cell "x1" is a Formula. Evaluate formula
        /// 
        /// Returns a double or throws ArgumentException
        /// </summary>

        private double Lookup(string name)
        {
            if (spreadsheet[name].Contents is double)
            {
                return (double) spreadsheet[name].Contents;
            }
            if (spreadsheet[name].Contents is string)
            {
                throw new ArgumentException("Variable is not a number.");
            }
            else if (spreadsheet[name].Contents is Formula)
            {
                Func<string, double> lkp = x => Lookup(x);
                Object obj = ((Formula)spreadsheet[name].Contents).Evaluate(lkp);
                if (obj is double)
                {
                    return (Double)obj;
                }
            }

            else
            {
                
                foreach(string var in dg.GetDependees(name))
                {
                    Lookup(var);
                }
            }
            return 2.0;
        }
    }
}
