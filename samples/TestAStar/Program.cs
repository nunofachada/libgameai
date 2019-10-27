using System;
using System.Collections.Generic;
using LibGameAI.PathFinding;

namespace LibGameAI.Samples.TestAStar
{
    class Program
    {
        static void Main()
        {
            IGraph g;

            Tuple<IGraph, float[]> data = GetData1();
            //ShowConns(g);
            IEnumerable<IConnection> conns =
                AStar.GetPath(data.Item1, 0, 5, data.Item2);
            ShowPath(conns);

        }

        static void ShowConns(IGraph g)
        {
            for (int i = 0; i < g.NumberOfNodes; i++)
            {
                Console.WriteLine($"** Node {i} **");
                foreach (Connection c in g.GetConnections(i))
                {
                    Console.WriteLine($"- from {c.FromNode} to {c.ToNode}: " +
                    c.Cost);
                }
            }
        }

        static void ShowPath(IEnumerable<IConnection> conns)
        {
            foreach (IConnection c in conns)
            {
                Console.WriteLine($"Node {c.FromNode} to {c.ToNode}: " +
                    c.Cost);
            }
        }

        static Tuple<IGraph, float[]> GetData1()
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


        static Tuple<IGraph, float[]> GetData2()
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
