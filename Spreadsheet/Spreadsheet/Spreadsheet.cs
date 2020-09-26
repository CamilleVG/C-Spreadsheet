// Written by Camille van Ginkel for PS4 assignment for CS 3500, September 2020
// Implements AbstractSpreadsheet interface written by Joe Zachary for CS 3500, September 2013

using SpreadsheetUtilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        ///<summary>
        ///Determines whether a string name for a variable meets standard variable name format.
        ///For example, "x", "_", "x2", "y_15", and "___" are all valid cell names, but
        /// "25", "2x", and other symbols are not.  Cell names are case sensitive, so "x" and "X" are
        /// different cell names.
        ///</summary>
        readonly Func<string, bool> IsValidName;

        /// <summary>
        /// Constructor creates an empty spreadsheet.
        /// </summary>
        public Spreadsheet()
        {
            dg = new DependencyGraph();
            spreadsheet = new Dictionary<string, Cell>();
           
            String varPattern = @"[a-zA-Z_](?: [a-zA-Z_]|\d)*";
            IsValidName = x => Regex.IsMatch(x, varPattern);
        }

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
        /// <param name="name">The name of the cell being changed</param>
        /// <param name="number">The contents that is input into the cell</param>
        /// <returns>A list of strings containing the names of all the cells that directly or indirectly depend on the value of 
        /// the cell being changed.  The cells are listed in the order that they need to be recalculated.</returns>
        /// <exception cref="InvalidNameException">Is thrown if name is null or invalid.</exception>
        public override IList<string> SetCellContents(string name, double number)
        {
            if (name is null || !IsValidName(name))
            {
                throw new InvalidNameException();
            }

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
                //If the cell is non-empty, change the contents of the already existing cell object
                if (spreadsheet.ContainsKey(name))
                {
                    spreadsheet[name].SetContents(formula);
                }
                //Otherwise, add the cell to spreadsheet
                else
                {
                    Cell cell = new Cell(formula);
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
                //If GetCellsToRecalculate throws CircularException it is caught bellow
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

    }
}
