/* Copyright (c) 2018-2023 Nuno Fachada and contributors
 * Distributed under the MIT License (See accompanying file LICENSE or copy
 * at http://opensource.org/licenses/MIT) */

using System;
using LibGameAI.PRNG;

namespace LibGameAI.Samples.PRNGs
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new LCG48(123);
            Console.WriteLine(r.Next(100));
        }
    }
}
