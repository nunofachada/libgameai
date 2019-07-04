using System;
using NGrams;
using System.Collections.Generic;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            int nValue = 5;
            //INGram<char> predictor = new HierarchNGram<char>(nValue, 3);
            INGram<char> predictor = new NGram<char>(nValue);
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

                if (listOfChars.Count == nValue)
                {
                    Console.WriteLine($"You entered '{listOfChars[nValue-1]}', I predicted '{prediction}'");
                    if (listOfChars[nValue-1] == prediction) yes++; else no++;
                    predictor.RegisterSequence(listOfChars.ToArray());
                    listOfChars.RemoveAt(0);
                    prediction = predictor.GetMostLikely(listOfChars.ToArray());
                 }
            } while (k != ConsoleKey.Escape);
            Console.WriteLine($"Correctly predicted: {((float) yes)/(yes+no):p}");
        }
    }
}
