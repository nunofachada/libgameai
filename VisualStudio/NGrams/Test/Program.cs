using System;
using NGrams;
using System.Collections.Generic;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //INGram<char> predictor = new HierarchNGram<char>(5, 3);
            INGram<char> predictor = new NGram<char>(5);
            List<char> listOfChars = new List<char>();
            ConsoleKey k;
            int yes = 0, no = 0;
            char prediction = default(char);
            do
            {

                k = Console.ReadKey(true).Key;
                switch (k)
                {
                    case ConsoleKey.RightArrow:
                        listOfChars.Add('R');
                        break;
                    case ConsoleKey.LeftArrow:
                        listOfChars.Add('L');
                        break;
                }

                if (listOfChars.Count == 6)
                {
                    Console.WriteLine($"You entered '{listOfChars[5]}', I predicted '{prediction}'");
                    if (listOfChars[5] == prediction) yes++; else no++;
                    predictor.RegisterSequence(listOfChars.ToArray());
                    listOfChars.RemoveAt(0);
                    prediction = predictor.GetMostLikely(listOfChars.ToArray());
                 }
            } while (k != ConsoleKey.Escape);
            Console.WriteLine($"Correctly predicted: {((float) yes)/(yes+no):p}");
        }
    }
}
