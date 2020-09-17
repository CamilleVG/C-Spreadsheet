using FormulaEvaluator;
using System;
using System.Collections.Generic;

namespace TestFormulaEvaluator
{

    int x = 6;
    public class Thing
    {
        public int x;
    }
    static void Main(string[] args)
    {
        string s1 = "3.00000000001";
        string s2 = "3.0";

        Console.WriteLine(s1 == s2);  //will return false;

        double d1 = Double.Parse(s1);
        double d2 = Double.Parse(s2);

        Console.WriteLine(d1 == d2); //wll return false
        //we will convert them to a double then back to a string
        //As far as the formula is concerned, the precision level of 
        //string determines if numbers are the same
        string backToString1 = d1.ToString();  //"3.000"
        string backToString2 = d2.ToString();  //"3.000"

        Console.WriteLine(backToString1 == backToString2); //will return true

        Thing t = new Thing();
        t.x = 5;
        Console.WriteLine();

     }

    static void ReplaceInt(ref Thing t)
    {
        t.x = 5;
    }





    /*
     * I Used this project for notes in discussion section.  I have not used the project for testing
     * Tests are located in UnitTestEvaluator
     * */



    //makes proxy spreadsheet object for testing
    //its basically a wrapper method around a dictionary



    /*For Future Reference Regex Expressions:

        '^' means beginning of string
        string pattern = "^[a-zA-Z]+[0-9]+$";
        one or more letters followed by one ore more digits
        dont put any space
        string x = "a1";

        //Regex.IsMatch(x, pattern);
        */


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
            return 1;
            //throw new ArgumentException("unkown variable");
        }

        public static int BasicLookup(string s)
        {
            if (s == "a4")
                return 1;
            throw new ArgumentException("unkown variable");
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
           // Console.WriteLine(Evaluator.Evaluate("5-3+a4", NoVarsLookup));
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

            //FakeSpreadsheet fake = new FakeSpreadsheet();
            //fake.AddCell("a3", 4);

            //FakeSpreadsheet empty = new FakeSpreadsheet();
           // Evaluator.Evaluate("5-a4", empty.GetCell);

            
            Console.Read();


        }
    }
}
