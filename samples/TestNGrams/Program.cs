/* Copyright (c) 2018-2023 Nuno Fachada and contributors
 * Distributed under the MIT License (See accompanying file LICENSE or copy
 * at http://opensource.org/licenses/MIT) */

using System;
using System.Collections.Generic;
using LibGameAI.NGrams;

namespace LibGameAI.Samples.TestNGrams
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
            Console.WriteLine("Press right/left arrows, ESC to quit");
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
