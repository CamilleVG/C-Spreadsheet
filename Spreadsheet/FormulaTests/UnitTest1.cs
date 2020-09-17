using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;

namespace FormulaTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Formula f = new Formula("5.6 - 3.6");
            
            double result = (double)f.Evaluate(//x=>0);

            //Assert.AreEqual(2.0, (double)f.Evaluate(x=>0));

            Assert.AreEqual(2.0, (double)f.Evaluate(x=>0), 1e-9);
            //(double expected, double actual, double delta)
            //use 1e-9 for all of your unit tests

        }

        [TestMethod]
        public void TestMethod2()
        {
            Formula f = new Formula("5/0");
            //"5/(3.3-2.2 -1.1)" does not throw error
            //Before dividing, check if divisor == 0
            //Dont care. Just check for exactly by divide by 0 (not close to 0)

            Assert.IsInstanceOfType(f.Evaluate(x => 0), typeof(FormulaError));
            //tests that it throws an exception



        }

    }
}
