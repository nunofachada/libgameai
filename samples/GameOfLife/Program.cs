/* Copyright (c) 2018-2024 Nuno Fachada and contributors
 * Distributed under the MIT License (See accompanying file LICENSE or copy
 * at http://opensource.org/licenses/MIT) */

using System;
using System.Threading;
using LibGameAI.PCG;

namespace LibGameAI.Samples.GameOfLife
{
    public class Program
    {
        private const int rows = 30;
        private const int cols = 50;

        private static void Main()
        {
            Console.Clear();
            ConsoleColor originalColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"===== Game Of Life ({rows}x{cols}) [Press Escape to exit] =====");

            CA2DBinaryRule rule = new CA2DBinaryRule("M,1/2,3/3");
            CA2D ca = new CA2D(rule, cols, rows, true);
            ca.InitRandom(new int[] { 0, 1 });

            Console.ForegroundColor = ConsoleColor.Yellow;

            while (true)
            {
                Console.SetCursorPosition(2, 2);
                for (int y = 0; y < ca.YDim; y++)
                {
                    for (int x = 0; x < ca.XDim; x++)
                    {
                        char c = ca[x, y] == 1 ? '#' : '.';
                        Console.Write(c);
                    }
                    Console.WriteLine();
                    Console.Write("  ");
                }
                ca.DoStep();

                if (Console.KeyAvailable)
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }
                Thread.Sleep(100);
            }

            Console.ForegroundColor = originalColor;
        }
    }
}
