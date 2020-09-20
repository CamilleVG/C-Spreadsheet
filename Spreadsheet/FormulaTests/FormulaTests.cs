using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;
using System.Collections.Generic;

//Copied all tests whose names starts with "Test" from PS1 Grading Tests created by Daniel Kopta
//
//@author Camille van Ginkel wrote and implemented units tests
namespace FormulaTests
{
    [TestClass]
    public class FormulaTests
    {
        /*
         * Tests Copied from PS1 GradingTests (not written by me)
         * Edited to test Formula's Evaluate method instead Evaluators Evaluate method
         */

        [TestMethod(), Timeout(5000)]
        [TestCategory("1")]
        public void TestSingleNumber()
        {
            Formula f = new Formula("5");
            Assert.AreEqual(5, (double) f.Evaluate(s => 0), 1e-9);
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("2")]
        public void TestSingleVariable()
        {
            Formula f = new Formula("X5");
            Assert.AreEqual(13, (double)f.Evaluate(s => 13), 1e-9);
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("3")]
        public void TestAddition()
        {
            Formula f = new Formula("5+3");
            Assert.AreEqual(8, (double)f.Evaluate(s => 0), 1e-9);
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("4")]
        public void TestSubtraction()
        {
            Formula f = new Formula("18-10");
            Assert.AreEqual(8, (double)f.Evaluate(s => 0), 1e-9);
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("5")]
        public void TestMultiplication()
        {
            Formula f = new Formula("2*4");
            Assert.AreEqual(8, (double)f.Evaluate(s => 0), 1e-9);
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("6")]
        public void TestDivision()
        {
            Formula f = new Formula("16/2");
            Assert.AreEqual(8, (double)f.Evaluate(s => 0), 1e-9);
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("7")]
        public void TestArithmeticWithVariable()
        {
            Formula f = new Formula("2+X1");
            Assert.AreEqual(6, (double)f.Evaluate(s => 4), 1e-9);
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("8")]
        public void TestUnknownVariable()
        {
            Formula f = new Formula("2+X1");
            object result = f.Evaluate(s => { throw new ArgumentException("Unknown variable"); });
            Assert.IsTrue(result is FormulaError);
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("9")]
        public void TestLeftToRight()
        {
            Formula f = new Formula("2*6+3");
            Assert.AreEqual(15, (double)f.Evaluate(s => 0), 1e-9);
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("10")]
        public void TestOrderOperations()
        {
            Formula f = new Formula("2+6*3");
            Assert.AreEqual(20, (double)f.Evaluate(s => 0), 1e-9);
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("11")]
        public void TestParenthesesTimes()
        {
            Formula f = new Formula("(2+6)*3");
            Assert.AreEqual(24, (double)f.Evaluate(s => 0), 1e-9);
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("12")]
        public void TestTimesParentheses()
        {
            Formula f = new Formula("2*(3+5)");
            Assert.AreEqual(16, (double)f.Evaluate(s => 0), 1e-9);
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("13")]
        public void TestPlusParentheses()
        {
            Formula f = new Formula("2+(3+5)");
            Assert.AreEqual(10, (double)f.Evaluate(s => 0), 1e-9);
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("14")]
        public void TestPlusComplex()
        {
            Formula f = new Formula("2+(3+5*9)");
            Assert.AreEqual(50, (double)f.Evaluate(s => 0), 1e-9);
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("15")]
        public void TestOperatorAfterParens()
        {
            Formula f = new Formula("(1*1)-2/2");
            Assert.AreEqual(0, (double)f.Evaluate(s => 0), 1e-9);
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("16")]
        public void TestComplexTimesParentheses()
        {
            Formula f = new Formula("2+3*(3+5)");
            Assert.AreEqual(26, (double)f.Evaluate(s => 0), 1e-9);
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("17")]
        public void TestComplexAndParentheses()
        {
            Formula f = new Formula("2+3*5+(3+4*8)*5+2");
            Assert.AreEqual(194, (double)f.Evaluate(s => 0), 1e-9);
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("26")]
        public void TestComplexMultiVar()
        {
            Formula f = new Formula("y1*3-8/2+4*(8-9*2)/10*x7");
            Assert.AreEqual(4, (double)f.Evaluate(s => (s == "x7") ? 1 : 4), 1e-9);
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("27")]
        public void TestComplexNestedParensRight()
        {
            Formula f = new Formula("x1+(x2+(x3+(x4+(x5+x6))))");
            Assert.AreEqual(6, (double)f.Evaluate(s => 1));
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("28")]
        public void TestComplexNestedParensLeft()
        {
            Formula f = new Formula("((((x1+x2)+x3)+x4)+x5)+x6");
            Assert.AreEqual(12, (double)f.Evaluate(s => 2));
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("29")]
        public void TestRepeatedVar()
        {
            Formula f = new Formula("a4-a4*a4/a4");
            Assert.AreEqual(0, (double)f.Evaluate(s => 3));
        }



        [TestMethod]
        public void CorrectInputDoesNotThrow()
        {
            Formula f = new Formula("5.6 - 3.6");

            double result = (double)f.Evaluate(x => 0);
            Assert.AreEqual(2.0, (double)f.Evaluate(x => 0), 1e-9);
            //(double expected, double actual, double delta)
            //use 1e-9 for all of your unit tests
        }
        [TestMethod]
        public void CorrectInputWithDecimalMultiplicationDoesNotThrow()
        {
            Formula f = new Formula("20/(1.50 + 2.50) + 5");

            double result = (double)f.Evaluate(x => 0);
            Assert.AreEqual(10.0, (double)f.Evaluate(x => 0), 1e-9);
            //(double expected, double actual, double delta)
            //use 1e-9 for all of your unit tests
        }

        [TestMethod]
        public void CorrectInputWithVarsDoesNotThrow()
        {
            Formula f = new Formula("20/(1.50 + 2.50) + 5 + x1*(x5 + x8)", s => s.ToUpper(), s => s.StartsWith("X"));

            double result = (double)f.Evaluate(x => 0);
            Assert.AreEqual(10.0, (double)f.Evaluate(x => 0), 1e-9);
            //(double expected, double actual, double delta)
            //use 1e-9 for all of your unit tests
        }

        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void UsingParenthesisForMultiplicationException()
        {
            Formula f1 = new Formula("(2+1)(2+2)");
        }

        [TestMethod]
        public void DivideBy0ReturnsFormulaError()
        {
            Formula f = new Formula("5/0");
            //"5/(3.3-2.2 -1.1)" does not throw error
            //Before dividing, check if divisor == 0
            //Dont care. Just check for exactly by divide by 0 (not close to 0)

            Assert.IsInstanceOfType(f.Evaluate(x => 0), typeof(FormulaError));
            //tests that it throws an exception
        }

        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void AtLeastOneTokenRule()
        {
            Formula f = new Formula("           ");
        }

        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void MissingLeftParenthesis()
        {
            Formula f = new Formula("4 + 5)");
        }

        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void BalancedParenthesis1()
        {
            Formula f = new Formula("(20/(1.50 + 2.50) + 5");
        }

        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void BalancedParenthesis2()
        {
            Formula f = new Formula("(4+5) + (10/(4-2)");
        }


        /// <summary>
        /// f.GetVariables should numerate the normalized versions of all of the variables that occur in this 
        /// formula.  No normalization may appear more than once in the enumeration, even 
        /// if it appears more than once in this Formula.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x+y*z", N, s => true).GetVariables() should enumerate "X", "Y", and "Z"
        /// </summary>
        /// 
        [TestMethod(), Timeout(5000)]
        public void GetVariables1()
        {
            Formula f = new Formula("x+y*z", s => s.ToUpper(), s => true);
            SortedSet<string> expectedVariables = new SortedSet<string>();
            expectedVariables.Add("X");
            expectedVariables.Add("Y");
            expectedVariables.Add("Z");
            SortedSet<string> actualVariables = new SortedSet<string>();
            foreach (string var in f.GetVariables())
            {
                actualVariables.Add(var);
            }
            Assert.IsTrue(expectedVariables.ToString().Equals(actualVariables.ToString()));
        }
        /// <summary>
        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x+X*z", N, s => true).GetVariables() should enumerate "X" and "Z".
        /// </summary>
        /// 
        [TestMethod(), Timeout(5000)]
        public void GetVariables2()
        {
            Formula f = new Formula("x+X*z", s => s.ToUpper(), s => true);
            SortedSet<string> expectedVariables = new SortedSet<string>();
            expectedVariables.Add("X");
            expectedVariables.Add("Z");
            SortedSet<string> actualVariables = new SortedSet<string>();
            foreach (string var in f.GetVariables())
            {
                actualVariables.Add(var);
            }
            Assert.IsTrue(expectedVariables.ToString().Equals(actualVariables.ToString()));
        }

        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x+X*z").GetVariables() should enumerate "x", "X", and "z".
        [TestMethod(), Timeout(5000)]
        public void GetVariables3()
        {
            Formula f = new Formula("x+X*z");
            SortedSet<string> expectedVariables = new SortedSet<string>();
            expectedVariables.Add("x");
            expectedVariables.Add("X");
            expectedVariables.Add("z");
            SortedSet<string> actualVariables = new SortedSet<string>();
            foreach (string var in f.GetVariables())
            {
                actualVariables.Add(var);
            }
            Assert.IsTrue(expectedVariables.ToString().Equals(actualVariables.ToString()));
        }

        /// <summary>
        /// Returns a string containing no spaces which, if passed to the Formula
        /// constructor, will produce a Formula f such that this.Equals(f).  All of the
        /// variables in the string should be normalized.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x + y", N, s => true).ToString() should return "X+Y"
        /// </summary>
        [TestMethod(), Timeout(5000)]
        public void ToStrings1()
        {
            Formula f = new Formula("x+y", s => s.ToUpper(), s => true);
            Assert.AreEqual(f.ToString(), "X+Y");
        }
        /// <summary>
        /// Tests ToStrings Method
        /// new Formula("x + Y").ToString() should return "x+Y"
        /// </summary>
        [TestMethod(), Timeout(5000)]
        public void ToStrings2()
        {
            Formula f = new Formula("x+Y");
            Assert.AreEqual(f.ToString(), "x+Y");
        }
        /// <summary>
        /// Returns a string containing no spaces which, if passed to the Formula
        /// constructor, will produce a Formula f such that this.Equals(f).  All of the
        /// variables in the string should be normalized.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        public void ToStrings3()
        {
            Formula F = new Formula("x+Y");
            Formula f = new Formula(F.ToString());
            Assert.IsTrue(F.Equals(f));
        }


        /// <summary>
        /// If obj is null or obj is not a Formula, returns false.  Otherwise, reports
        /// whether or not this Formula and obj are equal.
        /// 
        /// Two Formulae are considered equal if they consist of the same tokens in the
        /// same order.  To determine token equality, all tokens are compared as strings 
        /// except for numeric tokens and variable tokens.
        /// Numeric tokens are considered equal if they are equal after being "normalized" 
        /// by C#'s standard conversion from string to double, then back to string. This 
        /// eliminates any inconsistencies due to limited floating point precision.
        /// Variable tokens are considered equal if their normalized forms are equal, as 
        /// defined by the provided normalizer.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        ///  
        /// new Formula("x1+y2", N, s => true).Equals(new Formula("X1  +  Y2")) is true
        /// new Formula("x1+y2").Equals(new Formula("X1+Y2")) is false
        /// new Formula("x1+y2").Equals(new Formula("y2+x1")) is false
        /// new Formula("2.0 + x7").Equals(new Formula("2.000 + x7")) is true
        /// </summary>
        [TestMethod(), Timeout(5000)]
        public void EqualsWithExactSameStringInput()
        {

            Assert.IsTrue(new Formula("x+Y").Equals(new Formula("x+Y")));
        }
        [TestMethod(), Timeout(5000)]
        public void EqualsWithNormalizedVariables()
        {
            Assert.IsTrue(new Formula("x1+y2", s => s.ToUpper(), s => true).Equals(new Formula("X1+Y2")));
        }
        [TestMethod(), Timeout(5000)]
        public void EqualsWithDifferentCapitalizationInVariables()
        {
            Formula f = new Formula("x1+y2");
            Assert.IsFalse(f.Equals(new Formula("X1+Y2")));
        }
        [TestMethod(), Timeout(5000)]
        public void EqualsVariablesInDifferentOrder()
        {
            Assert.IsFalse(new Formula("x1+y2").Equals(new Formula("y2+x1")));
        }
        [TestMethod(), Timeout(5000)]
        public void EqualsWithEquaivalentDecimalsOfDifferentPrecision()
        {
            Formula f = new Formula("2.000+y2");
            Assert.IsTrue(new Formula("2.0 + x7").Equals(new Formula("2.000+x7")));
        }
        [TestMethod(), Timeout(5000)]
        public void EqualsWithNullObject()
        {
            Assert.IsFalse(new Formula("x+Y").Equals(null));
        }
        [TestMethod(), Timeout(5000)]
        public void EqualsOnObjectThatIsNotFormula()
        {
            Assert.IsFalse(new Formula("x+Y").Equals("%+!"));
        }
        [TestMethod(), Timeout(5000)]
        public void EqualsSymbolBothInputsAreNull()
        {
            Formula f1 = null;
            Formula f2 = null;
            Assert.IsTrue(f1 == f2);
        }
        [TestMethod(), Timeout(5000)]
        public void EqualsSymbolsFirstInputIsNull()
        {
            Formula f1 = null;
            Formula f2 = new Formula("1+2");
            Assert.IsFalse(f1 == f2);
        }
        [TestMethod(), Timeout(5000)]
        public void EqualsSymbolsSecondInputIsNull()
        {
            Formula f1 = new Formula("1+2");
            Formula f2 = null;
            Assert.IsFalse(f1 == f2);
        }
        [TestMethod(), Timeout(5000)]
        public void EqualsSymbolsBothValidInputsAreEqual()
        {
            Formula f1 = new Formula("1+2");
            Formula f2 = new Formula("1.0 + 2.0");
            Assert.IsTrue(f1 == f2);
        }
        [TestMethod(), Timeout(5000)]
        public void EqualsSymbolsValidInputsAreNOTEqual()
        {
            Formula f1 = new Formula("1+2");
            Formula f2 = new Formula("1.0 + 5");
            Assert.IsFalse(f1 == f2);
        }
        [TestMethod(), Timeout(5000)]
        public void NotEqualsSymbolsBothInputsNullReturnsFalse()
        {
            Formula f1 = null;
            Formula f2 = null;
            Assert.IsFalse(f1 != f2);
        }

        [TestMethod(), Timeout(5000)]
        public void NotEqualsSymbolsFirstInputIsNullReturnsTrue()
        {
            Formula f1 = null;
            Formula f2 = new Formula("12/6");
            Assert.IsTrue(f1 != f2);
        }
        [TestMethod(), Timeout(5000)]
        public void NotEqualsSymbolsSecondInputIsNullReturnsTrue()
        {
            Formula f1 = new Formula("12/6");
            Formula f2 = null;
            Assert.IsTrue(f1 != f2);
        }
        [TestMethod(), Timeout(5000)]
        public void NotEqualsSymbolsValidInputsAreEqualReturnsFalse()
        {
            Formula f1 = new Formula("1+2");
            Formula f2 = new Formula("1.0 + 2.0");
            Assert.IsFalse(f1 != f2);
        }
        [TestMethod(), Timeout(5000)]
        public void NotEqualsSymbolsValidInputsAreNotEqualReturnsTrue()
        {
            Formula f1 = new Formula("1+2");
            Formula f2 = new Formula("1.0 + 5");
            Assert.IsTrue(f1 != f2);
        }
        [TestMethod(), Timeout(5000)]
        public void GetHashCodeReturnsSameCodeForSameFormula()
        {
            Formula f1 = new Formula("1+2");
            Formula f2 = new Formula("1.0 + 2.0");
            int code1 = f1.GetHashCode();
            int code2 = f1.GetHashCode();
            Assert.IsTrue(code1.Equals(code2));
        }
        [TestMethod(), Timeout(5000)]
        public void GetHashCodeReturnsDifferentCodesForDifferentFormulas()
        {
            Formula f1 = new Formula("1+2");
            Formula f2 = new Formula("1.0 + 9");
            int code1 = f1.GetHashCode();
            int code2 = f2.GetHashCode();
            Assert.IsFalse(code1.Equals(code2));
        }
        [TestMethod(), Timeout(5000)]
        public void EvaluateLotsOfMultiplication()
        {
            Formula f1 = new Formula("1*2*3*4*5*6*7*8*(5+5)");
            Formula f2 = new Formula("(9-(10/(5-3)))");
            int code1 = f1.GetHashCode();
            int code2 = f2.GetHashCode();
            Assert.IsFalse(code1.Equals(code2));
        }

        /*
         * Test Methods for Formula's Evaluate method  
         */

        [TestMethod(), Timeout(5000)]
        public void EvaluateComplicatedParenthesis()
        {
            //
            Formula f1 = new Formula("(1+(2-2)+(3+(1+2))+(4+(2+(1+1))))");
            Assert.AreEqual(15, (double)f1.Evaluate(x => 0), 1e-9);
        }
        [TestMethod()]//, Timeout(5000)]
        public void EvaluateMultiplicationInsideOfParenthesis()
        {
            Formula f1 = new Formula("4*((10*1)*5)");
            Assert.AreEqual(200, (double)f1.Evaluate(x => 0), 1e-9);
        }
        [TestMethod()]//, Timeout(5000)]
        public void EvaluateDivisionInsideOfParenthesis()
        {
            Formula f1 = new Formula("1000/((50/2)/5)");
            Assert.AreEqual(200, (double)f1.Evaluate(x => 0), 1e-9);
        }
        [TestMethod(), Timeout(5000)]
        public void EvaluateAddingFactions()
        {
            Formula f1 = new Formula("1/2 + 3/4 + 1/4 - 1/2");
            Assert.AreEqual(1, (double)f1.Evaluate(x => 0), 1e-9);
        }
        [TestMethod(), Timeout(5000)]
        public void EvaluateAddingDecimals()
        {
            Formula f1 = new Formula("0.5 + 0.75 + 0.25 - 0.5");
            Assert.AreEqual(1, (double)f1.Evaluate(x => 0), 1e-9);
        }

        [TestMethod(), Timeout(5000)]
        public void EvaluateMultiplyingDecimals()
        {
            Formula f1 = new Formula("0.5 * 0.5 * 0.5");
            Assert.AreEqual(0.125, (double)f1.Evaluate(x => 0), 1e-9);
        }

        [TestMethod(), Timeout(5000)]
        public void EvaluateMultiplyingFractions()
        {
            Formula f1 = new Formula("1/2 * 1/2 * 1/2");
            Assert.AreEqual(0.125, (double)f1.Evaluate(x => 0), 1e-9);
        }

        [TestMethod(), Timeout(5000)]
        public void EvaluateWithVariables()
        {
            Formula f1 = new Formula("x1 + x2*x3 - x4 + x5/x6*1");
            Assert.AreEqual(2, (double)f1.Evaluate(x => 1), 1e-9);
        }

    }
}
