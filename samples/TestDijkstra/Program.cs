using System;
using System.Collections.Generic;
using LibGameAI.PathFinding;

namespace LibGameAI.Samples.TestDijkstra
{
    class Test
    {
        static void Main()
        {
            IGraph g;

            Console.WriteLine("=== Graph initialized with Adj. Matrix ===");
            g = TestAM();
            //ShowConns(g);
            ShowPath(Dijkstra.GetShortestPath(g, 0, 7));

            Console.WriteLine("=== Graph initialized with Adj. List ===");
            g = TestAL();
            //ShowConns(g);
            ShowPath(Dijkstra.GetShortestPath(g, 0, 6));

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

        static IGraph TestAM()
        {
            return new Graph(
                new float[,]
                {
                    { 0.0f, 0.2f, 0.0f, 1.2f, 0.0f, 0.0f, 9.5f, 0.0f},
                    { 0.1f, 0.0f, 0.0f, 0.0f, 3.1f, 0.0f, 0.0f, 0.0f},
                    { 0.0f, 2.3f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
                    { 4.5f, 0.0f, 0.0f, 0.0f, 3.5f, 0.0f, 0.0f, 0.0f},
                    { 0.0f, 0.0f, 2.1f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f},
                    { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.1f},
                    { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.7f, 0.0f, 5.2f},
                    { 0.0f, 0.0f, 0.0f, 0.0f, 0.3f, 0.0f, 0.0f, 0.0f}
                }
            );
        }

        static IGraph TestAL()
        {
            return new Graph(
                new IConnection[][]
                {
                    new IConnection[]
                    {
                        new Connection(1.3f, 0, 1),
                        new Connection(1.6f, 0, 2),
                        new Connection(3.3f, 0, 3)
                    },
                    new IConnection[]
                    {
                        new Connection(1.5f, 1, 4),
                        new Connection(1.9f, 1, 5)
                    },
                    new IConnection[]
                    {
                        new Connection(1.3f, 2, 3)
                    },
                    new IConnection[]
                    {
                    },
                    new IConnection[]
                    {
                    },
                    new IConnection[]
                    {
                        new Connection(1.4f, 5, 6)
                    },
                    new IConnection[]
                    {
                    }
                }
            );
        }
    }
}
