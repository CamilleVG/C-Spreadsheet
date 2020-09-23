using Microsoft.VisualStudio.TestTools.UnitTesting;
using SS;
using SpreadsheetUtilities;
using Microsoft.VisualStudio.TestPlatform.CoreUtilities.Extensions;
using System.Collections.Generic;

namespace SpreadsheetTests
{
    [TestClass]
    public class SpreadsheetTests
    {
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
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(CircularException))]
        public void SetCellContentsFormulaCircularexception()
        {
            Spreadsheet s = new Spreadsheet();
            Formula f = new Formula("c1 + b1");
            s.SetCellContents("c1", f);
        }
        /// <summary>
        /// Enumerates the names of all the non-empty cells in the spreadsheet.
        /// </summary>
        [TestMethod]
        public void GetNamesOfAllNonemptyCells()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("a1", 1.0);
            s.SetCellContents("b1", 2.0);
            s.SetCellContents("c1", 3.0);
            List<string> actualcells = new List<string>();
            foreach (string name in s.GetNamesOfAllNonemptyCells())
            {
                actualcells.Add(name);
            }
            List<string> expectedcells = new List<string>();
            expectedcells.Add("a1");
            expectedcells.Add("b1");
            expectedcells.Add("c1");
            Assert.IsTrue(actualcells.ToString().Equals(expectedcells.ToString()));
        }
    }
}
