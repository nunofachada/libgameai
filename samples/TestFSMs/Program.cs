/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * Author: Nuno Fachada
 * */

using System;
using System.Threading;
using System.Collections.Concurrent;
using LibGameAI.FSMs;

namespace LibGameAI.Samples.TestFSMs
{

    class Program
    {

        private static ConcurrentQueue<ConsoleKeyInfo> queue;
        private static bool bigEnemyVisible;
        private static bool smallEnemyVisible;

        static void Main(string[] args)
        {
            queue = new ConcurrentQueue<ConsoleKeyInfo>();
            bigEnemyVisible = false;
            smallEnemyVisible = false;
            Thread t = new Thread(KeyReader);
            t.Start();

            State stateOG = new State(
                "On Guard",
                () => Console.WriteLine("Enter On Guard State"),
                null,
                () => Console.WriteLine("Bye On Guard"));

            State stateF = new State(
                "Fight",
                () => Console.WriteLine("Enter Fight State"),
                null,
                () => Console.WriteLine("Bye Fight"));

            State stateRA = new State(
                "Run away",
                () => Console.WriteLine("Enter Runwaway State"),
                null,
                () => Console.WriteLine("Bye Runwaway"));

            stateOG.AddTransition(new Transition(
                () => smallEnemyVisible,
                () => Console.WriteLine("I see small enemy and I'm going to fight"),
                stateF));

            stateOG.AddTransition(new Transition(
                () => bigEnemyVisible,
                () => Console.WriteLine("I see big enemy and I'm running away"),
                stateRA));

            stateRA.AddTransition(new Transition(
                () => !bigEnemyVisible && !smallEnemyVisible,
                () => Console.WriteLine("I've escaped'"),
                stateOG));

            stateF.AddTransition(new Transition(
                () => bigEnemyVisible && smallEnemyVisible,
                () => Console.WriteLine("I'm losing the fight, asta la vista baby"),
                stateRA));

            StateMachine sm = new StateMachine(stateOG);

            while (true)
            {
                ConsoleKeyInfo cki;
                if (queue.TryDequeue(out cki))
                {
                    switch (cki.Key)
                    {
                        case ConsoleKey.B:
                            bigEnemyVisible = !bigEnemyVisible;
                            break;
                        case ConsoleKey.S:
                            smallEnemyVisible = !smallEnemyVisible;
                            break;
                    }
                }
                Action actions = sm.Update();
                actions?.Invoke();
            }
        }

        static void KeyReader()
        {
            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                queue.Enqueue(cki);
            }
        }
    }
}
