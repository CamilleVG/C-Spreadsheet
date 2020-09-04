using FormulaEvaluator;
using System;
using System.Collections.Generic;

namespace TestFormulaEvaluator
{
    //makes proxy spreadsheet object for testing
    //its basically a wrapper method around a dictionary
    public class FakeSpreadsheet
    {
        private Dictionary<string, int> cells = new Dictionary<string, int>();
        public void AddCell(string CellName, int cellVal)
        {
            cells[CellName] = cellVal;  //for that key set that value
        }
        public int GetCell(string s)
        {
            if (cells.ContainsKey(s))
                return cells[s];
            throw new ArgumentException("unkown cell");
        }
    }
    
    
    
    class Program
    {

        public static int NoVarsLookup(string s)
        {
            Console.WriteLine("lookup was invoked");
            
            throw new ArgumentException("unkown variable");
        }

        public static int BasicLookup(string s)
        {
            if (s == "a4")
                return 9;
            throw new ArgumentException("unkown variable");
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(Evaluator.Evaluate("5-3+a4", NoVarsLookup));
            //Do I add FormulaEvaluator as a project reference?

            try
            {
                Evaluator.Evaluate("5-3+a4", NoVarsLookup);
                //if it throws it will immediately jump down to the catch block
                Console.WriteLine("fail");
            }
            catch(ArgumentException e)
            {
                Console.WriteLine("pass");
            }

            //how it will eventually all fit together
           // Spreadsheet s = ;
            //Evaluator.Evaluate("...", s.GetCellValue);

            FakeSpreadsheet fake = new FakeSpreadsheet();
            fake.AddCell("a3", 4);

            FakeSpreadsheet empty = new FakeSpreashet();
            Evaluator.Evaluate("5-a4", empty.GetCell);

            
            Console.Read();


        }
    }
}
