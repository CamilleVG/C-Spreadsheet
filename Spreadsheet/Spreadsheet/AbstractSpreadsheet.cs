// Written by Joe Zachary for CS 3500, September 2013

using System;
using System.IO;
using System.Collections.Generic;
using SpreadsheetUtilities;

namespace SS
{
    
    /// <summary>
    /// Thrown to indicate that a change to a cell will cause a circular dependency.
    /// </summary>
    public class CircularException : Exception
    {
    }


    /// <summary>
    /// Thrown to indicate that a name parameter was either null or invalid.
    /// </summary>
    public class InvalidNameException : Exception
    {
    }

    /// <summary>
    /// <para>An AbstractSpreadsheet object represents the state of a simple spreadsheet.  A 
    /// spreadsheet consists of an infinite number of named cells.</para>
    /// <para>A string is a valid cell name if and only if:
    ///   (1) its first character is an underscore or a letter
    ///   (2) its remaining characters (if any) are underscores and/or letters and/or digits
    /// Note that this is the same as the definition of valid variable from the Formula class.</para>
    /// <para>For example, "x", "_", "x2", "y_15", and "___" are all valid cell names, but
    /// "25", "2x", and other symbols.  Cell names are case sensitive, so "x" and "X" are
    /// different cell names.</para>
    /// <para>A spreadsheet contains a cell corresponding to every possible cell name.  (This
    /// means that a spreadsheet contains an infinite number of cells.)  In addition to 
    /// a name, each cell has a contents and a value.  The distinction is important.</para>
    /// <para>The contents of a cell can be (1) a string, (2) a double, or (3) a Formula.  If the
    /// contents is an empty string, we say that the cell is empty.  (By analogy, the contents
    /// of a cell in Excel is what is displayed on the editing line when the cell is selected.)</para>
    /// <para>In a new spreadsheet, the contents of every cell is the empty string.</para>
    /// <para>The value of a cell can be (1) a string, (2) a double, or (3) a FormulaError.  
    /// (By analogy, the value of an Excel cell is what is displayed in that cell's position
    /// in the grid.)</para>
    /// <para>If a cell's contents is a string, its value is that string.</para>
    /// <para>If a cell's contents is a double, its value is that double.</para>
    /// <para>If a cell's contents is a Formula, its value is either a double or a FormulaError,
    /// as reported by the Evaluate method of the Formula class.  The value of a Formula,
    /// of course, can depend on the values of variables.  The value of a variable is the 
    /// value of the spreadsheet cell it names (if that cell's value is a double) or 
    /// is undefined (otherwise).</para>
    /// <para>Spreadsheets are never allowed to contain a combination of Formulas that establish
    /// a circular dependency.  A circular dependency exists when a cell depends on itself.
    /// For example, suppose that A1 contains B1*2, B1 contains C1*2, and C1 contains A1*2.
    /// A1 depends on B1, which depends on C1, which depends on A1.  That's a circular
    /// dependency.</para>
    /// </summary>
    public abstract class AbstractSpreadsheet
    {
        /// <summary>
        /// Enumerates the names of all the non-empty cells in the spreadsheet.
        /// </summary>
        public abstract IEnumerable<String> GetNamesOfAllNonemptyCells();


        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, returns the contents (as opposed to the value) of the named cell.  The return
        /// value should be either a string, a double, or a Formula.
        /// </summary>
        public abstract object GetCellContents(String name);


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
        public abstract IList<String> SetCellContents(String name, double number);

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
        public abstract IList<String> SetCellContents(String name, String text);

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
        /// </summary>
        public abstract IList<String> SetCellContents(String name, Formula formula);


        /// <summary>
        /// Returns an enumeration, without duplicates, of the names of all cells whose
        /// values depend directly on the value of the named cell.  In other words, returns
        /// an enumeration, without duplicates, of the names of all cells that contain
        /// formulas containing name.
        /// 
        /// <para>For example, suppose that A1 contains 3, B1 contains the formula A1 * A1, C1 contains the formula B1 + A1,
        /// D1 contains the formula B1 - C1.
        /// The direct dependents of A1, are B1 and C1.</para>
        /// </summary>
        protected abstract IEnumerable<String> GetDirectDependents(String name);


        /// <summary>
        /// Requires that names be non-null.  Also requires that if names contains s,
        /// then s must be a valid non-null cell name.
        /// 
        /// If any of the named cells are involved in a circular dependency,
        /// throws a CircularException.
        /// 
        /// Otherwise, returns an enumeration of the names of all cells whose values must
        /// be recalculated, assuming that the contents of each cell named in names has changed.
        /// The names are enumerated in the order in which the calculations should be done.  
        /// 
        /// For example, suppose that 
        /// A1 contains 5
        /// B1 contains 7
        /// C1 contains the formula A1 + B1
        /// D1 contains the formula A1 * C1
        /// E1 contains 15
        /// 
        /// If A1 and B1 have changed, then A1, B1, and C1, and D1 must be recalculated,
        /// and they must be recalculated in either the order A1,B1,C1,D1 or B1,A1,C1,D1.
        /// The method will produce one of those enumerations.
        /// 
        /// PLEASE NOTE THAT THIS METHOD DEPENDS ON THE ABSTRACT METHOD GetDirectDependents.
        /// IT WON'T WORK UNTIL GetDirectDependents IS IMPLEMENTED CORRECTLY.
        /// </summary>
        protected IEnumerable<String> GetCellsToRecalculate(ISet<String> names)
        {
            LinkedList<String> changed = new LinkedList<String>();
            HashSet<String> visited = new HashSet<String>();
            foreach (String name in names)
            {
                if (!visited.Contains(name))  //If the cell has not been visited, visit it and all of its dependents
                {
                    Visit(name, name, visited, changed);
                }
            }
            return changed;
        }


        /// <summary>
        /// A convenience method for invoking the other version of GetCellsToRecalculate
        /// with a singleton set of names.  See the other version for details.
        /// </summary>
        protected IEnumerable<String> GetCellsToRecalculate(String name)
        {
            return GetCellsToRecalculate(new HashSet<String>() { name });
        }


        /// <summary>
        /// A helper for the GetCellsToRecalculate() method that visits all the start cell and all of its dependents.
        /// It is a topological sort in that it adds the deepest cells from the starting cell of the dependency graph first to the beginning of changed List.
        /// Thus, the base starting cell ends up at the beginning and the deepest cells end up
        /// at the end of the list.
        /// </summary>
        /// <param name="start">The first cell is the cell that was input into the Visit() call in GetCellsToRecalculate()
        /// and it is the first cell that is visited.  The rest of its dependent cells are visted by recursion.</param>
        /// <param name="name">The current cell that is being visited</param>
        /// <param name="visited">The set of cells that have already been visited</param>
        /// <param name="changed">The list of cells whose values need to be recalculated in
        /// the order in which they need to be recalculated. The cells are ordered topologically. </param>
        private void Visit(String start, String name, ISet<String> visited, LinkedList<String> changed)
        {
            visited.Add(name);  //updates the set of cells that haved been visited
            foreach (String n in GetDirectDependents(name)) //for each cell that directly depends on the current cell being visited
            {
                if (n.Equals(start)) //if at any point in the recursion the start cell is a dependent of itself, then there is a circular dependency
                {
                    throw new CircularException();
                }
                else if (!visited.Contains(n))  //Otherwise, if the dependent cell has not been visited, visit it
                {
                    Visit(start, n, visited, changed);
                }
            }
            //After recursively visiting all dependent cells and adding them to the changed list
            changed.AddFirst(name);  //add the current cell to the beginning of the list of cells that need to be changed
        }

    }
}