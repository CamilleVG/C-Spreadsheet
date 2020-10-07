///Written by Camille van Ginkel for PS5 assignment in CS 3500, October 2020
///PS5 branch of spreadsheet repository Version 1.0.1

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SS;
using SpreadsheetUtilities;
using System.Collections.Generic;
using System;
using System.Linq;

namespace SpreadsheetTests
{
    [TestClass]
    public class SpreadsheetTests
    {
        /// <summary>
        /// Stress Tests!
        /// The following stress tests are from PS4 Grading tests.
        /// Tests have been edited to take in SetContentsOfCell()
        /// </summary>
        [TestMethod(), Timeout(2000)]
        [TestCategory("31")]
        public void TestStress1()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "=B1+B2");
            s.SetContentsOfCell("B1", "=C1-C2");
            s.SetContentsOfCell("B2", "=C3*C4");
            s.SetContentsOfCell("C1", "=D1*D2");
            s.SetContentsOfCell("C2", "=D3*D4");
            s.SetContentsOfCell("C3", "=D5*D6");
            s.SetContentsOfCell("C4", "=D7*D8");
            s.SetContentsOfCell("D1", "=E1");
            s.SetContentsOfCell("D2", "=E1");
            s.SetContentsOfCell("D3", "=E1");
            s.SetContentsOfCell("D4", "=E1");
            s.SetContentsOfCell("D5", "=E1");
            s.SetContentsOfCell("D6", "=E1");
            s.SetContentsOfCell("D7", "=E1");
            s.SetContentsOfCell("D8", "=E1");
            IList<String> cells = s.SetContentsOfCell("E1", "0");
            Assert.IsTrue(new HashSet<string>() { "A1", "B1", "B2", "C1", "C2", "C3", "C4", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "E1" }.SetEquals(cells));
        }

        // Repeated for extra weight
        [TestMethod(), Timeout(2000)]
        [TestCategory("32")]
        public void TestStress1a()
        {
            TestStress1();
        }
        [TestMethod(), Timeout(2000)]
        [TestCategory("33")]
        public void TestStress1b()
        {
            TestStress1();
        }
        [TestMethod(), Timeout(2000)]
        [TestCategory("34")]
        public void TestStress1c()
        {
            TestStress1();
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("35")]
        public void TestStress2()
        {
            Spreadsheet s = new Spreadsheet();
            ISet<String> cells = new HashSet<string>();
            for (int i = 1; i < 200; i++)
            {
                cells.Add("A" + i);

                Assert.IsTrue(cells.SetEquals(s.SetContentsOfCell("A" + i, "=A" + (i + 1))));
            }
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("36")]
        public void TestStress2a()
        {
            TestStress2();
        }
        [TestMethod(), Timeout(2000)]
        [TestCategory("37")]
        public void TestStress2b()
        {
            TestStress2();
        }
        [TestMethod(), Timeout(2000)]
        [TestCategory("38")]
        public void TestStress2c()
        {
            TestStress2();
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("39")]
        public void TestStress3()
        {
            Spreadsheet s = new Spreadsheet();
            for (int i = 1; i < 200; i++)
            {
                s.SetContentsOfCell("A" + i, "=A" + (i + 1));
            }
            try
            {
                s.SetContentsOfCell("A150", "=A50");
                Assert.Fail();
            }
            catch (CircularException)
            {
            }
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("40")]
        public void TestStress3a()
        {
            TestStress3();
        }
        [TestMethod(), Timeout(2000)]
        [TestCategory("41")]
        public void TestStress3b()
        {
            TestStress3();
        }
        [TestMethod(), Timeout(2000)]
        [TestCategory("42")]
        public void TestStress3c()
        {
            TestStress3();
        }


        [TestMethod()]//, Timeout(2000)]
        [TestCategory("43")]
        public void TestStress4()
        {
            Spreadsheet s = new Spreadsheet();
            for (int i = 0; i < 10; i++)
            {
                s.SetContentsOfCell("A" + i, "=A" + (i + 1));
            }
            LinkedList<string> firstCells = new LinkedList<string>();
            LinkedList<string> lastCells = new LinkedList<string>();
            for (int i = 0; i < 5; i++)
            {
                firstCells.AddFirst("A" + i);
                lastCells.AddFirst("A" + (i + 5));
            }
            IList<string> actual = s.SetContentsOfCell("A4", "25.0");
            Assert.IsTrue(s.SetContentsOfCell("A4", "25.0").SequenceEqual(firstCells));
            IList<string> actualLast = s.SetContentsOfCell("A9", "0");
            Assert.IsTrue(s.SetContentsOfCell("A9", "0").SequenceEqual(lastCells));
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("44")]
        public void TestStress4a()
        {
            TestStress4();
        }
        [TestMethod(), Timeout(2000)]
        [TestCategory("45")]
        public void TestStress4b()
        {
            TestStress4();
        }
        [TestMethod(), Timeout(2000)]
        [TestCategory("46")]
        public void TestStress4c()
        {
            TestStress4();
        }



        /// <summary>
        /// IsValidName(string name)
        /// Variables for a Spreadsheet are only valid if they are one or more letters followed 
        /// by one or more digits (numbers). This must now be enforced by the spreadsheet.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(InvalidNameException))]
        public void InvalidNameException()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a_1", "4.0");
        }
        /// <summary>
        /// IsValidName(string name)
        /// Variables for a Spreadsheet are only valid if they are one or more letters followed 
        /// by one or more digits (numbers). This must now be enforced by the spreadsheet.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(InvalidNameException))]
        public void InvalidNameExceptionUnderscore()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("_", "4.0");
        }
        /// <summary>
        /// IsValidName(string name)
        /// Variables for a Spreadsheet are only valid if they are one or more letters followed 
        /// by one or more digits (numbers). This must now be enforced by the spreadsheet.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(InvalidNameException))]
        public void InvalidNameExceptionNoLetter()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("4", "4.0");
        }
        /// <summary>
        /// IsValidName(string name)
        /// Variables for a Spreadsheet are only valid if they are one or more letters followed 
        /// by one or more digits (numbers). This must now be enforced by the spreadsheet.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(InvalidNameException))]
        public void InvalidNameExceptionNoNumber()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a", "4.0");
        }


        /// <summary>
        /// SetCellContents(string name, string text) returns list
        /// If name is null, throws an InvalidNameException.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(InvalidNameException))]
        public void SetContentsOfCellNullNameException()
        {
            Spreadsheet s = new Spreadsheet();
            string name = null;
            s.SetContentsOfCell(name, "Title");
        }

        /// <summary>
        /// SetCellContents(string name, double num);
        /// If name invalid, throws an InvalidNameException.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(InvalidNameException))]
        public void SetContentsOfCellInvalidNameException()
        {
            Spreadsheet s = new Spreadsheet();
            String name = "y_15";
            s.SetContentsOfCell(name, " 2.0");
        }

        /// <summary>
        /// SetContentsOfCell()
        /// If name invalid, throws an InvalidNameException.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(InvalidNameException))]
        public void SetContentsOfCellInvalidNameExceptionByValidator()
        {
            Func<string, string> normalizer = x => x;
            Func<string, bool> validator = x => char.IsUpper(x[0]);
            Spreadsheet s = new Spreadsheet(validator, normalizer, "1.0");
            String name = "a1";
            s.SetContentsOfCell(name, " 2.0");
        }

        /// <summary>
        /// SetContentsOfCell()
        /// If name invalid, throws an InvalidNameException.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        public void SetContentsOfCellNormalizerAllowsName()
        {
            Func<string, string> normalizer = x => x.ToUpper();
            Func<string, bool> validator = x => char.IsUpper(x[0]);
            Spreadsheet s = new Spreadsheet(validator, normalizer, "1.0");
            String name = "a1";
            s.SetContentsOfCell(name, " 2.0");
        }


        /// <summary>
        /// Returns the version information of the spreadsheet saved in the named file.
        /// If there are any problems opening, reading, or closing the file, the method
        /// should throw a SpreadsheetReadWriteException with an explanatory message.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        public void GetSavedVersion()
        {
            Spreadsheet s1 = new Spreadsheet();
            String filename = @"C:\Users\Camille\source\repos\spreadsheet-CamilleVG\Spreadsheet\SpreadsheetTests\save.txt";
            s1.Save(filename);
            Assert.IsTrue(s1.GetSavedVersion(filename).Equals("default"));

            Func<string, string> normalizer = x => x.ToUpper();
            Func<string, bool> validator = x => char.IsUpper(x[0]);
            Spreadsheet s2 = new Spreadsheet(validator, normalizer, "1.0");
            s2.Save(filename);
            Assert.IsTrue(s2.GetSavedVersion(filename).Equals("1.0"));

            Func<string, string> normalizer2 = x => x.ToLower();
            Func<string, bool> validator2 = x => char.IsLower(x[0]);
            Spreadsheet s3 = new Spreadsheet(filename, validator, normalizer, "1.0");
            s3.Save(filename);
            Assert.IsTrue(s3.GetSavedVersion(filename).Equals("1.0"));
        }

        /// <summary>
        /// Save (string filename)
        /// If an invalid vile path is input into Save() method, throws ReadWriteException(); 
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void SaveInvalidFilePathSpreadsheetReadWriteException()
        {
            Spreadsheet s = new Spreadsheet();
            s.Save("/missing/save.xml");
        }

        /// <summary>
        /// Save(string filename)
        /// If name is null, there should be a reading file error.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void SaveReadNameIsNullException()
        {
            Spreadsheet s = new Spreadsheet();
            String filename = null;
            s.Save(filename);
        }

        /// <summary>
        /// Save (string filename)
        /// If an invalid vile path is input into Save() method, throws ReadWriteException(); 
        /// </summary>
        [TestMethod(), Timeout(5000)]
        public void SaveWorksCorrectly()
        {
            Func<string, string> normalizer = x => x.ToUpper();
            Func<string, bool> validator = x => char.IsUpper(x[0]);
            Spreadsheet s = new Spreadsheet(validator, normalizer, "1.0");
            s.SetContentsOfCell("c1", "5.0");
            s.SetContentsOfCell("b1", "=c1*2"); //b1 = 10
            s.SetContentsOfCell("a1", "=b1*2"); //a1=20
            s.SetContentsOfCell("x1", "I'm a string");
            Assert.IsTrue(s.Changed);

            String filename = @"C:\Users\Camille\source\repos\spreadsheet-CamilleVG\Spreadsheet\SpreadsheetTests\save.txt";
            s.Save(filename);
            Assert.IsFalse(s.Changed);
        }


        /// <summary>
        /// Spreadsheet (string filename, validator, normalizer, version);
        /// It should read a saved spreadsheet from the file (see the Save method) and use it to construct a new spreadsheet.
        /// The new spreadsheet should use the provided validity delegate, normalization delegate, and version. Do not try to 
        /// implement loading from file until after we have discussed XML in class. See the Examples repository for an example
        /// of reading and writing XML files.
        /// 
        /// This test should correctly scan file from filename and creates a spreadsheet with the same cell, but normalized with it's own normalizer.
        /// You can check the resulting file in save.txt
        /// </summary>
        [TestMethod(), Timeout(5000)]
        public void SpreadsheetConstructerThatReadsFile()
        {
            Func<string, string> normalizer = x => x.ToUpper();
            Func<string, bool> validator = x => char.IsUpper(x[0]);
            Spreadsheet s = new Spreadsheet(validator, normalizer, "1.0");
            s.SetContentsOfCell("c1", "5.0");
            s.SetContentsOfCell("b1", "=c1*2"); //b1 = 10
            s.SetContentsOfCell("a1", "=b1*2"); //a1=20
            String filename = @"C:\Users\Camille\source\repos\spreadsheet-CamilleVG\Spreadsheet\SpreadsheetTests\save.txt";
            s.Save(filename);

            Func<string, string> normalizer2 = x => x.ToLower();
            Func<string, bool> validator2 = x => char.IsLower(x[0]);
            Spreadsheet s2 = new Spreadsheet(filename, validator2, normalizer2, "1.0");
            s2.Save(filename);
        }

        /// <summary>
        /// Spreadsheet (string filename, validator, normalizer, version);
        /// It should read a saved spreadsheet from the file (see the Save method) and use it to construct a new spreadsheet.
        /// The new spreadsheet should use the provided validity delegate, normalization delegate, and version. Do not try to 
        /// implement loading from file until after we have discussed XML in class. See the Examples repository for an example
        /// of reading and writing XML files.
        /// 
        /// This test should correctly scan file from filename and creates a spreadsheet with the same cell, but normalized with it's own normalizer.
        /// You can check the resulting file in save.txt
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void SpreadsheetConstructerBadFilePath()
        {
            Func<string, string> normalizer2 = x => x.ToLower();
            Func<string, bool> validator2 = x => char.IsLower(x[0]);
            Spreadsheet s2 = new Spreadsheet("/missing/save.xml", validator2, normalizer2, "4.0");
        }

        /// <summary>
        /// GetSavedVersion (string filename)
        /// If an invalid vile path is input into Save() method, throws ReadWriteException(); 
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void GetSaveVersionInvalidFilePathSpreadsheetReadWriteException()
        {
            Spreadsheet s = new Spreadsheet();
            s.GetSavedVersion("/missing/save.xml");
        }

        /// <summary>
        /// GetSavedVersion (string filename)
        /// </summary>
        [TestMethod(), Timeout(5000)]
        public void GetSaveVersionWorksCorrectly()
        {
            Func<string, string> normalizer = x => x.ToUpper();
            Func<string, bool> validator = x => char.IsUpper(x[0]);
            Spreadsheet s = new Spreadsheet(validator, normalizer, "2.0");
            s.SetContentsOfCell("c1", "5.0");
            s.SetContentsOfCell("b1", "=c1*2"); //b1 = 10
            s.SetContentsOfCell("a1", "=b1*2"); //a1=20

            String filename = @"C:\Users\Camille\source\repos\spreadsheet-CamilleVG\Spreadsheet\SpreadsheetTests\save.txt";
            s.Save(filename);
            Assert.IsTrue(s.GetSavedVersion(filename).Equals("2.0"));
        }

        /// <summary>
        /// GetCellValue(string name)
        /// If the contents of the cell is a formula, returns the evaluated formula.  
        /// If it is a valid formula, returns double
        /// </summary>
        [TestMethod(), Timeout(5000)]
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
        /// If the input formula is dependent on other formulas, correctly gets values from those formulas.
        /// If it is a valid formula, returns double
        /// </summary>
        [TestMethod(), Timeout(5000)]
        public void GetCellValueOfFormulaLookupRecursion()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("b1", "=c1+1.0"); //8+1
            s.SetContentsOfCell("c1", "=d1+2.0"); //6+2
            s.SetContentsOfCell("d1", "=e1"); //6
            s.SetContentsOfCell("e1", "=f1*2"); //3*2
            s.SetContentsOfCell("f1", "=3.0");
            s.SetContentsOfCell("a1", "=b1 + 2"); //9+11
            // a1 is depndent on b1 -> c1 -> d1 -> e1 -> f1 which is the base and is a double
            // b1 is set before c1, so it must be recalculated after c1 is set
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
        /// GetCellValue(string name)
        /// If the contents of the cell is a formula, returns the evaluated formula.  
        /// If it is a valid formula, returns double
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(InvalidNameException))]
        public void GetCellValueGivenBadName()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("b1", "=c1+1.0");
            s.GetCellValue(null);
        }

        /// <summary>
        /// GetCellValue(string name)
        /// If the contents of the cell is a formula, returns the evaluated formula.  
        /// If it is a valid formula, returns double
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(InvalidNameException))]
        public void GetCellValueGivenInvalidName()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("b1", "=c1+1.0");
            s.GetCellValue("_y");
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
        /// If name meets valid name requirments but that cell has not been set in spreadsheet, it is an empty cell.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        public void GetCellContentsEmptyCell()
        {
            Spreadsheet s = new Spreadsheet();
            String name = "a1";
            Assert.IsTrue(s.GetCellContents(name).Equals("")) ;
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
            Assert.IsTrue(d == 2.0);
        }
        [TestMethod(), Timeout(5000)]
        public void GetCellContentsReturnsString()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", "Title");
            string contents = (string)s.GetCellContents("a1");
            Assert.IsTrue(contents.Equals("Title"));
        }
        [TestMethod(), Timeout(5000)]
        public void GetCellContentsReturnsFormula()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", "=2+3");
            Formula contents = (Formula)s.GetCellContents("a1");
            Assert.IsTrue(contents.Equals(new Formula("2+3")));
        }

        /// SetContentsOfCell(string name, string contents) returns list
        /// If the cell already had previously been set, it changes the current set contents.  
        /// </summary>
        [TestMethod(), Timeout(5000)]
        public void DoubleSetContentsOfCellAfterCellIsAlreadySet()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", "2.0");
            s.SetContentsOfCell("a1", "10.0");
            Assert.IsTrue(s.GetCellContents("a1").Equals(10.0));
            Assert.IsTrue(s.GetCellContents("a1") is double);
        }

        /// <summary>
        /// SetContentsOfCell(string name, string contents) returns list
        /// The contents of the named cell becomes number.  The method returns a
        /// list consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// list {A1, B1, C1} is returned.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        public void DoubleSetContentsOfCellCorrectImplementation()
        {
            Spreadsheet s = new Spreadsheet();
            string B1 = "=A1*2";
            string C1 = "=B1+A1";
            s.SetContentsOfCell("B1", B1);
            s.SetContentsOfCell("C1", C1);
            IList<string> actualDependents = s.SetContentsOfCell("A1", "2.0");
            IList<string> expectedDependents = new List<string>();
            expectedDependents.Add("A1");
            expectedDependents.Add("B1");
            expectedDependents.Add("C1");
            Assert.IsTrue(actualDependents.SequenceEqual(expectedDependents));
        }


        /// <summary>
        /// SetContentsOfCell(string name, string contents) returns list
        /// If the string given is empty of whitespace, that cell is considered empty
        /// </summary>
        [TestMethod(), Timeout(5000)]
        public void SetContentsofCellEmptyCell()
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
        /// SetContentsOfCell(string name, string contents) returns list
        /// If the cell already had previously been set, it changes the current set contents.  
        /// </summary>
        [TestMethod(), Timeout(5000)]
        public void StringSetContentsOfCellAfterCellIsAlreadySet()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", "Title");
            s.SetContentsOfCell("a1", "New Title");
            Assert.IsTrue(s.GetCellContents("a1").Equals("New Title"));
        }

        /// <summary>
        /// SetContentsOfCell(string name, string contents) returns list
        /// The contents of the named cell becomes text.  The method returns a
        /// list consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// list {A1, B1, C1} is returned.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        public void StringSetContentsOfCellCorrectImplementation()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("c1", "=b1+2");
            s.SetContentsOfCell("b1", "=a1+2");
            string name = "a1";
            IList<string> actualDependents = s.SetContentsOfCell(name, "Title");
            Assert.IsTrue(s.GetCellContents("a1").Equals("Title"));
            IList<string> expectedDependents = new List<string>();
            expectedDependents.Add("a1");
            expectedDependents.Add("b1");
            expectedDependents.Add("c1");
            Assert.IsTrue(actualDependents.SequenceEqual(expectedDependents));
        }

        /// <summary>
        /// SetContentsOfCell(string name, string contents) returns list
        /// If the contents parameter is, throws an ArgumentNullException.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetContentsOfCellArgumentNullException()
        {
            String f = null;
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", f);
        }


        /// <summary>
        /// SetContentsOfCell(string name, "=formula") returns list
        /// If changing the contents of the named cell to be the formula would cause a 
        /// circular dependency, throws a CircularException, and no change is made to the spreadsheet.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(CircularException))]
        public void FormulaSetContentsOfCellCircularException()
        {
            Spreadsheet s = new Spreadsheet();
            String f = "=c1 + b1";
            s.SetContentsOfCell("c1", f);
        }

        /// <summary>
        /// SetContentsOfCell(string name, "=formula") returns list
        /// If changing the contents of the named cell to be the formula would cause a 
        /// circular dependency, throws a CircularException, and no change is made to the spreadsheet.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(CircularException))]
        public void FormulaSetContentsOfCellCircularExceptionDeeperDependency()
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
        /// SetContentsOfCell(string name, "=formula") returns list
        /// If the cell already had previously been set, it changes the current set contents.  
        /// </summary>
        [TestMethod(), Timeout(5000)]
        public void FormulaSetContentsOfCellAfterCellIsAlreadySet()
        {
            Spreadsheet s = new Spreadsheet();
            Formula expected = new Formula("40/10");
            s.SetContentsOfCell("a1", "=2+2");
            s.SetContentsOfCell("a1", "=40/10");
            Formula actual = (Formula)s.GetCellContents("a1");
            Assert.IsTrue(actual.Equals(expected));
        }

        /// <summary>
        /// SetContentsOfCell(string name, "=formula") returns list
        /// The contents of the named cell becomes formula.  The method returns a
        /// list consisting of name plus the names of all other cells whose value depends,
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// list {A1, B1, C1} is returned.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        public void FormulaSetContentsOfCellWorksCorrectly()
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

        [TestMethod(), Timeout(5000)]
        public void SetContentsOfCellWithEmptyText()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", "   ");
            Assert.IsTrue(s.GetNamesOfAllNonemptyCells().Count().Equals(0));
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
            Assert.IsTrue((s2 == "a1" || s2 == "b1" || s2 == "c1") && s2 != s1);
            Assert.IsTrue((s3 == "a1" || s3 == "b1" || s3 == "c1") && (s3 != s1) && (s3 != s2));
        }


    }

}
