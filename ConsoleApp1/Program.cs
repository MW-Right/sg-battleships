using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Alphabet Array
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] alpha = alphabet.ToCharArray();
            //Selection Conversion for columns
            string userInput = Console.ReadLine();
            char columnLetter = Convert.ToChar(userInput.Substring(0, 1));
            int columnNumber = 0;
            foreach (var i in alphabet)
            {
                if (columnLetter == i)
                {
                    columnNumber = alphabet.IndexOf(i);
                }
            }
            Console.WriteLine(columnNumber);
        }
    }
}
