using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;
using System.Collections.Generic;
namespace FormulaTests
{
    [TestClass]
    public class FormulaTests
    {
        [TestMethod]
        public void CorrectInput1()
        {
            Formula f = new Formula("5.6 - 3.6");
            
            double result = (double)f.Evaluate(x=>0);
            Assert.AreEqual(2.0, (double)f.Evaluate(x=>0), 1e-9);
            //(double expected, double actual, double delta)
            //use 1e-9 for all of your unit tests
        }
        [TestMethod]
        public void CorrectInput2()
        {
            Formula f = new Formula("20/(1.50 + 2.50) + 5");

            double result = (double)f.Evaluate(x => 0);
            Assert.AreEqual(10.0, (double)f.Evaluate(x => 0), 1e-9);
            //(double expected, double actual, double delta)
            //use 1e-9 for all of your unit tests
        }

        [TestMethod]
        public void CorrectInputWithVars1()
        {
            Formula f = new Formula("20/(1.50 + 2.50) + 5 + x1*(x5 + x8)", s => s.ToUpper(), s => s.StartsWith("X"));

            double result = (double)f.Evaluate(x => 0);
            Assert.AreEqual(10.0, (double)f.Evaluate(x => 0), 1e-9);
            //(double expected, double actual, double delta)
            //use 1e-9 for all of your unit tests
        }

        [TestMethod]
        public void DivideBy0()
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
            Formula f = new Formula("x+y", s=> s.ToUpper(), s=> true);
            Assert.AreEqual(f.ToString(), "X+Y");
        }
        /// <summary>
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
            Assert.IsTrue(new Formula("x1+y2", s => s.ToUpper(), s=> true).Equals(new Formula("X1+Y2")));
        }
        //[TestMethod()], Timeout(5000)]
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
        [TestMethod()]//, Timeout(5000)]
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

    }
}
