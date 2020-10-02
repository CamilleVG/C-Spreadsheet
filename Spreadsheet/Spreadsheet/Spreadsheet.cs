// Written by Camille van Ginkel for PS4 assignment for CS 3500, September 2020
// Implements AbstractSpreadsheet interface written by Joe Zachary for CS 3500, September 2013

using SpreadsheetUtilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace SS
{
    /// <inheritdoc/>
    public class Spreadsheet : AbstractSpreadsheet
    {
        /// <summary>
        /// Maps the name of a cell to a Cell object.  The cell object holds the value and contents of a cell.
        /// It only maps cells whose contents have been set.  It does not hold they keys of empty cells.
        /// </summary>
        readonly Dictionary<string, Cell> spreadsheet;

        /// <summary>
        /// Tracks the dependencies of the non-empty cells in spreadsheet in a DAG.
        /// If a cell "t" contents is set to a formula that contains a variable to another cell "s", then it is 
        /// said that "t" depends on "s". 
        /// </summary>
        readonly DependencyGraph dg;

        /// <summary>
        /// Constructor creates an empty spreadsheet.
        /// </summary>
        public Spreadsheet() : base(x=> true, x=> x, "default")
        {
            dg = new DependencyGraph();
            spreadsheet = new Dictionary<string, Cell>();
            this.Changed = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isValid"></param>
        /// <param name="normalize"></param>
        /// <param name="version"></param>
        public Spreadsheet(Func<string, bool> isValid, Func<string, string> normalize, string version) : base(isValid, normalize, version)
        {
            dg = new DependencyGraph();
            spreadsheet = new Dictionary<string, Cell>();
            this.Changed = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="isValid"></param>
        /// <param name="normalize"></param>
        /// <param name="version"></param>
        public Spreadsheet(String filePath, Func<string, bool> isValid, Func<string, string> normalize, string version) : base(isValid, normalize, version)
        {
            //It should read a saved spreadsheet from the file (see the Save method) and use it to construct a new spreadsheet.
            //The new spreadsheet should use the provided validity delegate, normalization delegate, and version. Do not try to 
            //implement loading from file until after we have discussed XML in class. See the Examples repository for an example
            //of reading and writing XML files.
            this.Changed = false;
        }

        ///<summary>
        ///Determines whether a string name for a variable meets standard variable name format.
        ///For example, "x", "_", "x2", "y_15", and "___" are all valid cell names, but
        /// "25", "2x", and other symbols are not.  Cell names are case sensitive, so "x" and "X" are
        /// different cell names.
        ///</summary>
        private bool IsValidName(String x)
        {
            String varPattern = @"[a-zA-Z_](?: [a-zA-Z_]|\d)*";
            bool result = Regex.IsMatch(x, varPattern);
            return result;
        }
        
        
        ///  <inheritdoc/>
        public override bool Changed { get; protected set; }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, returns the contents (as opposed to the value) of the named cell.  The return
        /// value should be either a string, a double, or a Formula.
        /// </summary>
        /// <param name="name">The name of the cell whose contents is needed.</param>
        /// <returns>The contents that was input into the cell.</returns>
        public override object GetCellContents(string name)
        {
            if (name is null || !IsValidName(name) || !spreadsheet.ContainsKey(name))
            {
               throw new InvalidNameException();
            }
            return spreadsheet[name].Contents;
        }
        /// <inheritdoc/>
        public override object GetCellValue(string name)
        {
            if (name is null || !IsValidName(name) || !spreadsheet.ContainsKey(name))
            {
                throw new InvalidNameException();
            }
            return spreadsheet[name].Value;
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

        /// <inheritdoc/>
        public override string GetSavedVersion(string filename)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override void Save(string filename)
        {
            /// <summary>
            /// Writes the contents of this spreadsheet to the named file using an XML format.
            /// The XML elements should be structured as follows:
            /// 
            /// <spreadsheet version="version information goes here">
            /// 
            /// <cell>
            /// <name>cell name goes here</name>
            /// <contents>cell contents goes here</contents>    
            /// </cell>
            /// 
            /// </spreadsheet>
            /// 
            /// There should be one cell element for each non-empty cell in the spreadsheet.  
            /// If the cell contains a string, it should be written as the contents.  
            /// If the cell contains a double d, d.ToString() should be written as the contents.  
            /// If the cell contains a Formula f, f.ToString() with "=" prepended should be written as the contents.
            /// 
            /// If there are any problems opening, writing, or closing the file, the method should throw a
            /// SpreadsheetReadWriteException with an explanatory message.
            /// </summary>
            
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        /// <param name="name">The name of the cell being changed</param>
        /// <param name="number">The contents that is input into the cell</param>
        /// <returns>A list of strings containing the names of all the cells that directly or indirectly depend on the value of 
        /// the cell being changed.  The cells are listed in the order that they need to be recalculated.</returns>
        /// <exception cref="InvalidNameException">Is thrown if name is null or invalid.</exception>
        protected override IList<string> SetCellContents(string name, double number)
        {
            if (spreadsheet.ContainsKey(name)) //If the spreadsheet has a the named non-empty cell, edit contents of cell
            {
                spreadsheet[name].SetContents(number); 
            }
            else //Otherwise, create a new cell
            {
                Cell cell = new Cell(number);
                spreadsheet.Add(name, cell);
            }

            //Return list of all direct and indirect dependents
            IList<string> Dependents = new List<string>();
            foreach (string dependent in GetCellsToRecalculate(name)) 
            {
                Dependents.Add(dependent);
            }
            return Dependents; 
        }


        /// <inheritdoc/>
        /// <param name="name">The name of the cell whose contents is being set.</param>
        /// <param name="text">The string contents being input into the cell.</param>
        /// <returns>A list of strings containing the names of all the cells that directly or indirectly depend on the value of 
        /// the cell being changed.  The cells are listed in the order that they need to be recalculated.</returns>
        /// <exception cref="InvalidNameException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        protected override IList<string> SetCellContents(string name, string text)
        { 
            //If the input text is empty, the cell is considered empty.  Thus do not create cell.
            if (!(string.IsNullOrWhiteSpace(text) || string.IsNullOrEmpty(text)))
            {
                if (spreadsheet.ContainsKey(name))
                {
                    spreadsheet[name].SetContents(text);
                }
                else
                {
                    Cell cell = new Cell(text);
                    spreadsheet.Add(name, cell);
                }
            }
            //Return list of direct and indirect dependents in the order that they need to be recalulated.
            IList<string> Dependents = new List<string>();
            foreach (string n in GetCellsToRecalculate(name))
            {
                Dependents.Add(n);
            }
            return Dependents;
        }


        /// <inheritdoc/>
        /// /// <param name="name">The name of the cell whose contents is being set.</param>
        /// <param name="formula">The formula contents being input into the cell.</param>
        /// <returns>A list of strings containing the names of all the cells that directly or indirectly depend on the value of 
        /// the cell being changed.  The cells are listed in the order that they need to be recalculated.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidNameException"></exception>
        protected override IList<string> SetCellContents(string name, Formula formula)
        {
            try
            {
                //If the cell is non-empty, change the contents of the already existing cell object
                if (spreadsheet.ContainsKey(name))
                {
                    spreadsheet[name].SetContents(formula);
                }
                //Otherwise, add the cell to spreadsheet
                else
                {
                    Cell cell = new Cell(formula, x => Lookup(x));
                    //To instantiate a cell with a formula, the second parameter passes in the lookup method that 
                    //is defined in this spreadsheet in order to evaluate the values of the variables in the formula
                    spreadsheet.Add(name, cell);
                }
                //Since the input is a formula, the cell could depend on other cells
                //Update the dependency graph dg.
                foreach (string dependee in formula.GetVariables())
                {
                    Debug.Assert(IsValidName(dependee));
                    dg.AddDependency(dependee, name);
                }
                //Return list of all dependent cells
                //If GetCellsToRecalculate throws CircularException it is caught below
                IList<string> Dependents = new List<string>();
                foreach (string dependent in GetCellsToRecalculate(name))
                {
                    Dependents.Add(dependent);
                }
                return Dependents;
            }
            catch
            {
                //If CicularException is caught, undo changes to spreadsheet
                spreadsheet.Remove(name);
                foreach (string dependee in formula.GetVariables())
                {
                    dg.RemoveDependency(dependee, name);
                }
                throw new CircularException();
            }
        }

        /// <inheritdoc/>
        /// /// <summary>
        /// If content is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, if content parses as a double, the contents of the named
        /// cell becomes that double.
        /// 
        /// Otherwise, if content begins with the character '=', an attempt is made
        /// to parse the remainder of content into a Formula f using the Formula
        /// constructor.  There are then three possibilities:
        /// 
        ///   (1) If the remainder of content cannot be parsed into a Formula, a 
        ///       SpreadsheetUtilities.FormulaFormatException is thrown.
        ///       
        ///   (2) Otherwise, if changing the contents of the named cell to be f
        ///       would cause a circular dependency, a CircularException is thrown,
        ///       and no change is made to the spreadsheet.
        ///       
        ///   (3) Otherwise, the contents of the named cell becomes f.
        /// 
        /// Otherwise, the contents of the named cell becomes content.
        /// 
        /// If an exception is not thrown, the method returns a list consisting of
        /// name plus the names of all other cells whose value depends, directly
        /// or indirectly, on the named cell. The order of the list should be any
        /// order such that if cells are re-evaluated in that order, their dependencies 
        /// are satisfied by the time they are evaluated.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// list {A1, B1, C1} is returned.
        /// </summary>
        public override IList<string> SetContentsOfCell(string name, string content)
        {
            if (content is null)
            {
                throw new ArgumentNullException();
            }
            if (name is null || !IsValidName(name))
            {
                throw new InvalidNameException();
            }
            IList<string> Dependents = new List<string>();  //the list of the names of all direct and indirect dependent cells
            double num;
            content = content.Trim();
            if (Double.TryParse(content, out num))
            {
                Dependents = SetCellContents(name, num);
            }
            else if ((!string.IsNullOrWhiteSpace(content) || (!string.IsNullOrEmpty(content))))
            {
                if (content[0].Equals('='))
                {
                    Dependents = SetCellContents(name, new Formula(content.Substring(1)));
                }
            }
            else
            {
                //Other wise, try setting cell contents as a string
                Dependents = SetCellContents(name, content);
            }
            Changed = true;
            return Dependents;
        }

        /// <inheritdoc/>
        /// <param name="name">The name of the cell that is the dependee of all cells returned.</param>
        /// <returns>Enumeration of direct dependents of given cell</returns>
        protected override IEnumerable<string> GetDirectDependents(string name)
        {
            //If the cell does not have dependents, do not return anything
            if (!dg.HasDependents(name))
            {
                yield break;
            }
            //Otherwise, enumerate each dependent
            else
            {
                foreach (String n in dg.GetDependents(name))
                {
                    yield return n;
                }
            }
        }
        /// <summary>
        /// Given the name of a variable, it returns the value of that variable.
        /// </summary>
        /// <param name="variable"></param>
        /// <returns></returns>
        private double Lookup(string variable)
        {
            variable = Normalize(variable);
            if (GetCellContents(variable) is Double)
            {
                return (double)spreadsheet[variable].Value;
            }
            else if (GetCellContents(variable) is Formula)
            {
                Formula f = (Formula)spreadsheet[variable].Contents;
                object eval = f.Evaluate(x => this.Lookup(x));
                if (eval is double)
                {
                    double result = (double)eval; 
                    return result;
                }
                else //if the result if a FormulaError
                {
                    throw new ArgumentException("");
                }
            }
            else
            {
                throw new ArgumentException("One of the variables in formula is a string.");
            }
        }

    }
}
