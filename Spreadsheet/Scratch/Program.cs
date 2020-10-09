using System;
using System.Text.RegularExpressions;
/// <summary>
/// Let's add a comment to my solution and practice swapping between the master branch
/// and the new PS5 branch.
/// </summary>

namespace Scratch
{
    class Program
    {
        static void Main(string[] args)
        {
            //Practicing Regex Expressions
            Console.WriteLine("Run Works!");

            String dollarPattern = "I saw ([0-9]|like, tons of) dogs today";
            String dollar = "I saw dogs today";

            string varpattern = @"^[A-Z]([1-9]|[1-9][1-9])$";//([1 - 9] | 1[0 - 9] | 2[0 - 5])
            string cell = "B66";


            if (Regex.IsMatch(cell, varpattern))
            {
                Console.WriteLine(cell + " matches");
            }

        }
    }
}
