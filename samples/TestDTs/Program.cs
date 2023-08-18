/* Copyright (c) 2018-2023 Nuno Fachada and contributors
 * Distributed under the MIT License (See accompanying file LICENSE or copy
 * at http://opensource.org/licenses/MIT) */

using System;
using LibGameAI.DecisionTrees;

namespace LibGameAI.Samples.TestDTs
{
    class Program
    {
        static void Main(string[] args)
        {
            ActionNode an = new ActionNode(TestAction);
            an.Execute();
        }

        private static void TestAction()
        {
            Console.WriteLine("Success!");
        }
    }
}
