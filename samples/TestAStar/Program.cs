/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * Author: Nuno Fachada
 * */

using System;
using System.Collections.Generic;
using LibGameAI.PathFinding;

namespace LibGameAI.Samples.TestAStar
{
    class Program
    {
        private static void Main()
        {
            //IGraph g;

            Tuple<IGraph, float[]> data = GetData1();
            //ShowConns(g);
            IEnumerable<IConnection> conns =
                AStar.GetPath(data.Item1, 0, 5, data.Item2);
            ShowPath(conns);

        }

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
