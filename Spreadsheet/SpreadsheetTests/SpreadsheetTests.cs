///Written by Camille van Ginkel for PS4 assignment in CS 3500, September 2020

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SS;
using SpreadsheetUtilities;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SpreadsheetTests
{
    [TestClass]
    public class SpreadsheetTests 
    {

        /// <summary>
        /// GetCellValue(string name)
        /// If the value of the cell is a double, returns double
        /// </summary>
        [TestMethod(), Timeout(5000)]
        public void SaveInvalidFilePathSpreadsheetReadWriteException()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", "4.0");
            double actual = (double) s.GetCellValue("a1");
            Assert.IsTrue(actual == 4.0);
        }



        /// <summary>
        /// GetCellValue(string name)
        /// If the contents of the cell is a formula, returns the evaluated formula.  
        /// If it is a valid formula, returns double
        /// </summary>
        [TestMethod()]//, Timeout(5000)]
        public void GetCellValueOfFormula()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("b1", "5.0");
            s.SetContentsOfCell("c1", "3.0");
            s.SetContentsOfCell("a1", "=b1 + c1");
            double actual = (double)s.GetCellValue("a1");
            Assert.IsTrue(actual == 8.0);
        }

        /// <summary>
        /// GetCellValue(string name)
        /// If the contents of the cell is a formula, returns the evaluated formula.  
        /// If it is a valid formula, returns double
        /// </summary>
        [TestMethod(), Timeout(5000)]
        public void GetCellValueOfFormulaRecursion()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("b1", "=c1+1.0");
            s.SetContentsOfCell("c1", "=d1+ 2.0");
            s.SetContentsOfCell("d1", "=e1");
            s.SetContentsOfCell("e1", "=f1*2");
            s.SetContentsOfCell("f1", "=3.0");
            s.SetContentsOfCell("a1", "=b1 + 2"); //9.0 +2 == 11
            double actual = (double)s.GetCellValue("a1");
            Assert.IsTrue(actual == 11.0);
        }

        /// <summary>
        /// GetCellValue(string name)
        /// If the contents of the cell is a formula, returns the evaluated formula.  
        /// If it is a valid formula, returns double
        /// </summary>
        [TestMethod(), Timeout(5000)]
        public void GetCellValueOfFormulaReturnsFormulaError()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("b1", "=c1+1.0");
            s.SetContentsOfCell("c1", "I'm a string");
            s.SetContentsOfCell("a1", "=b1 + c1"); //= 9.0 + 8.0 = 17.0
            object actual = s.GetCellValue("a1");
            Assert.IsTrue(actual is FormulaError);
        }

        /// <summary>
        /// Save(string)
        /// </summary>
        /// 
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

        /// <summary>
        /// Save(string name)
        /// If name is null, throws an InvalidNameException.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void FixMe()
        {
            Spreadsheet s = new Spreadsheet();
            String filename = "/missing/save.xml";
            s.Save(filename);
        }

        /// <summary>
        /// Save(string filename)
        /// If name is null, there should be a reading file error.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void SaveReadException()
        {
            Spreadsheet s = new Spreadsheet();
            String filename = null;
            s.Save(filename);
        }

        /// <summary>
        /// GetCellContents(string name) returns object;
        /// If name is invalid, throws an InvalidNameException.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(InvalidNameException))]
        public void GetCellContentsInvalidNameException()
        {
            Spreadsheet s = new Spreadsheet();
            String name = "!00";
            s.GetCellContents(name);
        }
        /// <summary>
        /// GetCellContents(string name) returns object;
        /// If name meets valid name requirments but that cell has not been set in spreadsheet, throws an InvalidNameException.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(InvalidNameException))]
        public void GetCellContentsEmptyCell()
        {
            Spreadsheet s = new Spreadsheet();
            String name = "a1";
            s.GetCellContents(name);
        }

        /// <summary>
        /// GetCellContents(string name) returns object;
        /// Returns the contents (as opposed to the value) of the named cell.  The return
        /// value should be either a string, a double, or a Formula.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        public void GetCellContentsReturnsDouble()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", "2.0");
            double d = (double)s.GetCellContents("a1");
        }
        [TestMethod(), Timeout(5000)]
        public void GetCellContentsReturnsString()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", "Title");
            string contents = (string) s.GetCellContents("a1");
        }
        [TestMethod(), Timeout(5000)]
        public void GetCellContentsReturnsFormula()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", "=2+3");
            Formula contents = (Formula)s.GetCellContents("a1");
            Assert.IsTrue(contents.Equals(new Formula("2+3")));
        }

        /// <summary>
        /// SetCellContents(string name, double num);
        /// If name is null, throws an InvalidNameException.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(InvalidNameException))]
        public void SetCellContentsDoubleNullNameException()
        {
            Spreadsheet s = new Spreadsheet();
            String name = null;
            s.SetContentsOfCell(name, "2.0  ");
        }

        /// <summary>
        /// SetCellContents(string name, double num);
        /// If name invalid, throws an InvalidNameException.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(InvalidNameException))]
        public void SetCellContentsDoubleInvalidNameException()
        {
            Spreadsheet s = new Spreadsheet();
            String name = "%00";
            s.SetContentsOfCell(name," 2.0");
        }

        /// <summary>
        /// SetCellContents(string name, double number) returns list
        /// If the cell already had previously been set, it changes the current set contents.  
        /// </summary>
        [TestMethod(), Timeout(5000)]
        public void SetCellContentsDoubleAfterCellIsAlreadySet()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", "2.0");
            s.SetContentsOfCell("a1", "10.0");
            Assert.IsTrue(s.GetCellContents("a1").Equals(10.0));
            Assert.IsTrue(s.GetCellContents("a1") is double);
        }

        /// <summary>
        /// SetCellContents(string name, double num);
        /// The contents of the named cell becomes number.  The method returns a
        /// list consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// list {A1, B1, C1} is returned.
        /// </summary>
        //[TestMethod(), Timeout(5000)]
        //public void SetCellContentsDoubleCorrectImplementation()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    Formula B1 = new Formula("A1*2");
        //    Formula C1 = new Formula("B1+A1");
        //    s.SetContentsOfCell("B1", B1);
        //    s.SetCellContents("C1", C1);
        //    IList<string> actualDependents = s.SetCellContents("A1", 2.0);
        //    IList<string> expectedDependents = new List<string>();
        //    expectedDependents.Add("A1");
        //    expectedDependents.Add("B1");
        //    expectedDependents.Add("C1");
        //    Assert.IsTrue(actualDependents.SequenceEqual(expectedDependents));
        //}
        /// <summary>
        /// SetCellContents(string name, string text) returns list
        /// If text is null, throws an ArgumentNullException.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetCellContentsStringArgumentNullException()
        {
            Spreadsheet s = new Spreadsheet();
            string text = null;
            s.SetContentsOfCell("a1", text);
        }

        /// <summary>
        /// SetCellContents(string name, string text) returns list
        /// If name is null, throws an InvalidNameException.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(InvalidNameException))]
        public void SetCellContentsStringNullNameException()
        {
            Spreadsheet s = new Spreadsheet();
            string name = null;
            s.SetContentsOfCell(name, "Title");
        }

        /// <summary>
        /// SetCellContents(string name, string text) returns list
        /// If name is invalid, throws an InvalidNameException.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(InvalidNameException))]
        public void SetCellContentsStringInvalidNameException()
        {
            Spreadsheet s = new Spreadsheet();
            string name = "#1!";
            s.SetContentsOfCell(name, "Title");
        }

        /// <summary>
        /// SetCellContents(string name, string text) returns list
        /// If the string given is empty of whitespace, that cell is considered empty
        /// </summary>
        [TestMethod(), Timeout(5000)]
        public void SetCellContentsStringEmptyCell()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", "");
            s.SetContentsOfCell("b1", "         ");
            List<string> actualList = new List<string>();
            foreach (string n in s.GetNamesOfAllNonemptyCells())
            {
                actualList.Add(n);
            }
            Assert.IsTrue(actualList.Count == 0);
        }

        /// <summary>
        /// SetCellContents(string name, string text) returns list
        /// If the cell already had previously been set, it changes the current set contents.  
        /// </summary>
        [TestMethod()]//, Timeout(5000)]
        public void SetCellContentsStringAfterCellIsAlreadySet()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", "Title");
            s.SetContentsOfCell("a1", "New Title");
            Assert.IsTrue(s.GetCellContents("a1").Equals("New Title"));
        }

        /// <summary>
        /// SetCellContents(string name, string text) returns list
        /// The contents of the named cell becomes text.  The method returns a
        /// list consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// list {A1, B1, C1} is returned.
        /// </summary>
        //[TestMethod(), Timeout(5000)]
        //public void SetCellContentsStringCorrectImplementation()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    s.SetCellContents("c1", new Formula("b1+2"));
        //    s.SetCellContents("b1", new Formula("a1+2"));
        //    string name = "a1";
        //    IList<string> actualDependents = s.SetCellContents(name, "Title");
        //    Assert.IsTrue(s.GetCellContents("a1").Equals("Title"));
        //    IList<string> expectedDependents = new List<string>();
        //    expectedDependents.Add("a1");
        //    expectedDependents.Add("b1");
        //    expectedDependents.Add("c1");
        //    Assert.IsTrue(actualDependents.SequenceEqual(expectedDependents));
        //}

        /// <summary>
        /// SetCellContents(string name, Formula f)
        /// If the formula parameter is null, throws an ArgumentNullException.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetCellContentsFormulaArgumentNullException()
        {
            String f = null;
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", f);
        }

        
        /// <summary>
        /// SetCellContents(String name, Formula f)
        /// If name is null, throws an InvalidNameException.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(InvalidNameException))]
        public void SetCellContentsFormulaNullNameException()
        {
            String f = "=A1 + B1";
            Spreadsheet s = new Spreadsheet();
            string name = null;
            s.SetContentsOfCell(name, f);
        }

        /// <summary>
        /// SetCellContents(String name, Formula f)
        /// If name is , throws an InvalidNameException.
        /// </summary>
        [TestMethod()]//, Timeout(5000)]
        [ExpectedException(typeof(InvalidNameException))]
        public void SetCellContentsFormulaInvalidNameException()
        {
            String f = "=A1 + B1";
            Spreadsheet s = new Spreadsheet();
            string name = "@%!";
            s.SetContentsOfCell(name, f);
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
            String f = "=c1 + b1";
            s.SetContentsOfCell("c1", f);
        }

        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(CircularException))]
        public void SetCellContentsFormulaCircularExceptionDeeperDependency()
        {
            Spreadsheet s = new Spreadsheet();
            String A1 = "=c1*2";
            s.SetContentsOfCell("a1", A1);
            String B1 = "=1+a1";
            s.SetContentsOfCell("b1", B1);
            String C1 = "  =b1+1";
            s.SetContentsOfCell("c1", C1);
        }

        /// <summary>
        /// SetCellContents(string name, Formula f) returns list
        /// If the cell already had previously been set, it changes the current set contents.  
        /// </summary>
        [TestMethod(), Timeout(5000)]
        public void SetCellContentsFormulaAfterCellIsAlreadySet()
        {
            Spreadsheet s = new Spreadsheet();
            Formula expected = new Formula("40/10");
            s.SetContentsOfCell("a1", "=2+2");
            s.SetContentsOfCell("a1", "=40/10");
            Formula actual = (Formula)s.GetCellContents("a1");
            Assert.IsTrue(actual.Equals(expected));
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
        [TestMethod()]//, Timeout(5000)]
        public void SetCellContentsFormulaWorksCorrectly()
        {
            Spreadsheet s = new Spreadsheet();
            String B1 = "=A1*2";
            String C1 = "=B1+A1";
            String A1 = "=1+2";
            s.SetContentsOfCell("A1", A1);
            s.SetContentsOfCell("B1", B1);
            s.SetContentsOfCell("C1", C1);
            IList<string> actualDependents = s.SetContentsOfCell("A1", A1);
            IList<string> expectedDependents = new List<string>();
            expectedDependents.Add("A1");
            expectedDependents.Add("B1");
            expectedDependents.Add("C1");
            Assert.IsTrue(expectedDependents.SequenceEqual(actualDependents));
        }
        
        /// <summary>
        /// Enumerates the names of all the non-empty cells in the spreadsheet.
        /// </summary>
        [TestMethod]
        public void GetNamesOfAllNonemptyCells()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", "1.0");
            s.SetContentsOfCell("b1", "2.0");
            s.SetContentsOfCell("c1", "3.0");
            List<string> actualcells = new List<string>();
            foreach (string name in s.GetNamesOfAllNonemptyCells())
            {
                actualcells.Add(name);
            }
            List<string> expectedcells = new List<string>();
            expectedcells.Add("a1");
            expectedcells.Add("b1");
            expectedcells.Add("c1");
            Assert.IsTrue(actualcells.SequenceEqual(expectedcells));
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
            s.SetContentsOfCell("a1", "=b1+c1");
            s.SetContentsOfCell("c1", "=b1-1");
            s.SetContentsOfCell("b1", "=d1+2");
            
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
