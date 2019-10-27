using System;
using LibGameAI.DecisionTrees;

namespace LibGameAI.Samples.TestDTs
{
    class Program
    {
        static void Main(string[] args)
        {
            GameAction ga = new GameAction(TestAction);
            ga.DoGameAction();
        }

        private static void TestAction()
        {
            Console.WriteLine("Success!");
        }
    }
}
