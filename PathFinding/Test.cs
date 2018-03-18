using System;
using System.Collections.Generic;
using System.Text;

namespace PathFinding
{
    class Test
    {
        static void Main()
        {
            IGraph graph = new Graph(
                new IConnection[][] {
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

            foreach (Connection c in graph.PathfindDijkstra(0, 6))
            {
                Console.WriteLine($"Node {c.FromNode} to {c.ToNode}: " +
                    c.Cost);
            }
        }

    }
}
