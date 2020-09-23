using Microsoft.VisualStudio.TestTools.UnitTesting;
using SS;
using SpreadsheetUtilities;
using System.Collections.Generic;
using System;

namespace SpreadsheetTests
{
    [TestClass]
    public class SpreadsheetTests 
    {

        /// <summary>
        /// SetCellContents(string name, double num);
        /// If name is null, throws an InvalidNameException.
        /// </summary>

        /// <summary>
        /// SetCellContents(string name, double num);
        /// If name invalid, throws an InvalidNameException.
        /// </summary>

        /// 
        /// Otherwise, the contents of the named cell becomes number.  The method returns a
        /// list consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// list {A1, B1, C1} is returned.
        /// </summary>
        /// <summary>
        /// SetCellContents(string name, Formula f)
        /// If the formula parameter is null, throws an ArgumentNullException.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetCellContentsFormulaArgumentNullException()
        {
            Formula f = null;
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("a1", f);
        }

        
        /// <summary>
        /// SetCellContents(String name, Formula f)
        /// If name is null, throws an InvalidNameException.
        /// </summary>
        [TestMethod(), Timeout(5000)
        [ExpectedException(typeof(InvalidNameException))]
        public void SetCellContentsFormulaNullNameException()
        {
            Formula f = new Formula("A1 + B1");
            Spreadsheet s = new Spreadsheet();
            string name = null;
            s.SetCellContents(name, f);
        }

        /// <summary>
        /// SetCellContents(String name, Formula f)
        /// If name is , throws an InvalidNameException.
        /// </summary>
        [TestMethod()]//, Timeout(5000)]
        [ExpectedException(typeof(InvalidNameException))]
        public void SetCellContentsFormulaInvalidNameException()
        {
            Formula f = new Formula("A1 + B1");
            Spreadsheet s = new Spreadsheet();
            string name = "@%!";
            s.SetCellContents(name, f);
        }

        /// <summary>
        /// SetCellContents(String name, Formula f)
        /// If changing the contents of the named cell to be the formula would cause a 
        /// circular dependency, throws a CircularException, and no change is made to the spreadsheet.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(CircularException))]
        public void SetCellContentsFormulaCircularException()
        {
            Spreadsheet s = new Spreadsheet();
            Formula f = new Formula("c1 + b1");
            s.SetCellContents("c1", f);
        }

        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(CircularException))]
        public void SetCellContentsFormulaCircularExceptionFurtherDownDependency()
        {
            Spreadsheet s = new Spreadsheet();
            Formula f1 = new Formula("1+a1");
            s.SetCellContents("b1", f1);
            Formula f2 = new Formula("b1+1");
            s.SetCellContents("c1", f2);
            foreach (string name in s.GetNamesOfAllNonemptyCells())
            {
                Assert.IsTrue(name.Equals("b1"));
            }
        }

        /// <summary>
        /// SetCellContents(String name, Formula f)
        /// The contents of the named cell becomes formula.  The method returns a
        /// list consisting of name plus the names of all other cells whose value depends,
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// list {A1, B1, C1} is returned.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        public void SetCellContentsFormulaWorksCorrectly()
        {
            Spreadsheet s = new Spreadsheet();
            Formula B1 = new Formula("A1*2");
            Formula C1 = new Formula("B1+A1");
            Formula A1 = new Formula("1+2");
            s.SetCellContents("B1", B1);
            s.SetCellContents("C1", C1);
            IList<string> actualDependents = s.SetCellContents("A1", A1);
            IList<string> expectedDependents = new List<string>();
            expectedDependents.Add("A1");
            expectedDependents.Add("B1");
            expectedDependents.Add("C1");
            Assert.IsTrue(actualDependents.ToString().Equals(expectedDependents.ToString()));
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

        /// <summary>
        /// Checks that the GetEnumerator method of the IEnumerable GetNamesOfAllNonEmptyCells works correctly
        ///</summary>
        [TestMethod()]
        public void GetNamesOfAllNonemptyCellsEnumeratorTest()
        {
            Spreadsheet s = new Spreadsheet();

            IEnumerator<string> e = s.GetNamesOfAllNonemptyCells().GetEnumerator();
            Assert.IsFalse(e.MoveNext());

            e = s.GetNamesOfAllNonemptyCells().GetEnumerator();
            s.SetCellContents("a1", new Formula("b1+c1"));
            s.SetCellContents("c1", new Formula("b1-1"));
            s.SetCellContents("b1", new Formula("d1+2"));
            
            Assert.IsTrue(e.MoveNext());
            String s1 = e.Current;
            Assert.IsTrue(e.MoveNext());
            String s2 = e.Current;
            Assert.IsTrue(e.MoveNext());
            String s3 = e.Current;
            Assert.IsTrue(s1 == "a1" || s1 == "c1" || s1 == "b1");
            Assert.IsTrue((s2 == "a1" || s2 == "b1" || s2 == "c1") && s2 != s1 );
            Assert.IsTrue((s3 == "a1" || s3 == "b1" || s3 == "c1") && (s3 != s1) && (s3 != s2));
        }
    }
}
