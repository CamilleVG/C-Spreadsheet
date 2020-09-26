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
            Console.WriteLine("Run Works!");

            String dollarPattern = "I saw ([0-9]|like, tons of) dogs today";
            String dollar = "I saw dogs today";
            if (Regex.IsMatch(dollar, dollarPattern))
            {
                Console.WriteLine(dollar);
            }

        }
    }
}
