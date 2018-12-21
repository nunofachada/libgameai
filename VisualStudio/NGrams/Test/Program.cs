using System;
using NGrams;
using System.Collections.Generic;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            INGram<char> predictor = new NGram<char>(3);
            List<char> listOfChars = new List<char>();
            ConsoleKey k;
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

                if (listOfChars.Count == 4)
                {
                    Console.WriteLine($"You entered '{listOfChars[3]}', I predicted '{prediction}'");
                    predictor.RegisterSequence(listOfChars.ToArray());
                    listOfChars.RemoveAt(0);
                    prediction = predictor.GetMostLikely(listOfChars.ToArray());
                 }
            } while (k != ConsoleKey.Escape);

        }
    }
}
