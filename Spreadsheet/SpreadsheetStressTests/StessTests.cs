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
                s.SetContentsOfCell("A" + i, "=A" + (i+1));
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
    }
}
