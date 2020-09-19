using Microsoft.VisualStudio.TestTools.UnitTesting;
using FormulaEvaluator;

namespace UnitTestEvaluator
{
    [TestClass]
    public class UnitTest1
    {
        public static double NoVarsLookup(string s)
        {
            //Console.WriteLine("lookup was invoked");
            return 1.0;
            //throw new ArgumentException("unkown variable");
        }
        
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(9, Evaluator.Evaluate("5 + 4*1", NoVarsLookup));
        }
        [TestMethod]
        //[ExpectedException(typeof(...)]
        public void TestMethod2()
        {
            Assert.AreEqual(6, Evaluator.Evaluate("24/4", NoVarsLookup));
        }
        [TestMethod]
        public void TestMethod3()
        {
            Assert.AreEqual(10, Evaluator.Evaluate("(3+2)*2", NoVarsLookup));
        }
        [TestMethod]
        public void TestMethod4()
        {
            Assert.AreEqual(101, Evaluator.Evaluate("100*a1 +1", NoVarsLookup));
        }
        [TestMethod]
        public void TestMethod5()
        {
            Assert.AreEqual(99, Evaluator.Evaluate("100 * aaahhhhknmj9000 -1       ", NoVarsLookup));
        }
    }
}
