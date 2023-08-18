/* Copyright (c) 2018-2023 Nuno Fachada and contributors
 * Distributed under the MIT License (See accompanying file LICENSE or copy
 * at http://opensource.org/licenses/MIT) */

using System;
using System.Collections.Generic;
using LibGameAI.PathFinding;

namespace LibGameAI.Samples.TestAStar
{
    class Program
    {
        private static Tuple<IGraph, float[]> data;

        private static void Main()
        {
            IPathFinder pathFinder = new AStarPathFinder(HeuristicFunc);

            data = GetData1();
            IEnumerable<IConnection> conns =
                pathFinder.FindPath(data.Item1, 0, 5);
            ShowPath(conns);

        }

        private static float HeuristicFunc(int node) => data.Item2[node];

        private static void ShowConns(IGraph g)
        {
            int maxNode = 0;
            for (int i = 0; i <= maxNode; i++)
            {
                Console.WriteLine($"** Node {i} **");
                foreach (Connection c in g.GetConnections(i))
                {
                    if (c.ToNode > maxNode) maxNode = c.ToNode;
                    Console.WriteLine($"- from {c.FromNode} to {c.ToNode}: " +
                    c.Cost);
                }
            }
        }

        private static void ShowPath(IEnumerable<IConnection> conns)
        {
            foreach (IConnection c in conns)
            {
                Console.WriteLine($"Node {c.FromNode} to {c.ToNode}: " +
                    c.Cost);
            }
        }

        private static Tuple<IGraph, float[]> GetData1()
        {
            return new Tuple<IGraph, float[]>(
                new Graph(
                    new IConnection[][]
                    {
                        new IConnection[]
                        {
                            new Connection(2f, 0, 1),
                            new Connection(5f, 0, 2),
                        },
                        new IConnection[]
                        {
                            new Connection(2f, 1, 3),
                            new Connection(4f, 1, 4)
                        },
                        new IConnection[]
                        {
                            new Connection(5f, 2, 5)
                        },
                        new IConnection[]
                        {
                            new Connection(3f, 3, 4)
                        },
                        new IConnection[]
                        {
                            new Connection(2f, 4, 5)
                        },
                        new IConnection[]
                        {
                        }
                    }
                ),
                new float[] { 6f, 2f, 3f, 1f, 1f, 0f }
             );
        }

        private static Tuple<IGraph, float[]> GetData2()
        {
            return new Tuple<IGraph, float[]>(
                new Graph(
                    new IConnection[][]
                    {
                        new IConnection[]
                        {
                            new Connection(1.3f, 0, 1),
                            new Connection(1.1f, 0, 2),
                        },
                        new IConnection[]
                        {
                            new Connection(1.5f, 1, 3),
                            new Connection(1.7f, 1, 4)
                        },
                        new IConnection[]
                        {
                            new Connection(1.5f, 2, 4),
                            new Connection(1.6f, 2, 5)
                        },
                        new IConnection[]
                        {
                        },
                        new IConnection[]
                        {
                            new Connection(1.4f, 4, 6)
                        },
                        new IConnection[]
                        {
                        },
                        new IConnection[]
                        {
                        }
                    }
                ),
                new float[] { 4.2f, 3.2f, 3.7f, 2.8f, 1.6f, 1.4f, 0.0f }
             );
        }
    }
}
