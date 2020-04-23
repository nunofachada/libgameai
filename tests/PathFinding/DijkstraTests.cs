/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * Author: Nuno Fachada
 * */

using System.Linq;
using System.Collections.Generic;
using Xunit;
using LibGameAI.PathFinding;

namespace Tests.PathFinding
{
    public class DijkstraTests
    {
        // Returns a collection of (graph, shortest path) pairs
        public static IEnumerable<object[]> GetAdjMatricesAndShortestPaths()
        {
            // A graph represented by an adjacency matrix and its shortest path
            yield return new object[]
            {
                // Graph represented by an adjacency matrix
                new Graph(new float[,]
                {
                    { 0.0f, 0.2f, 0.0f, 1.2f, 0.0f, 0.0f, 9.5f, 0.0f},
                    { 0.1f, 0.0f, 0.0f, 0.0f, 3.1f, 0.0f, 0.0f, 0.0f},
                    { 0.0f, 2.3f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
                    { 4.5f, 0.0f, 0.0f, 0.0f, 3.5f, 0.0f, 0.0f, 0.0f},
                    { 0.0f, 0.0f, 2.1f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f},
                    { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.1f},
                    { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.7f, 0.0f, 5.2f},
                    { 0.0f, 0.0f, 0.0f, 0.0f, 0.3f, 0.0f, 0.0f, 0.0f}
                }),
                // Shortest path
                new (int from, int to)[]
                { (0, 1), (1, 4), (4, 2), (2, 5), (5, 7) }
            };
            // A graph represented by an adjacency list and its shortest path
            yield return new object[]
            {
                // Graph represented by an adjacency matrix
                new Graph(
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
                        new IConnection[] { new Connection(1.3f, 2, 3) },
                        new IConnection[] { },
                        new IConnection[] { },
                        new IConnection[] { new Connection(1.4f, 5, 6) },
                        new IConnection[] { }
                    }
                ),
                // Shortest path
                new (int from, int to)[]
                { (0, 1), (1, 5), (5, 6) }
            };
        }

        // Check if shortest path is found with Dijkstra algorithm
        [Theory]
        [MemberData(nameof(GetAdjMatricesAndShortestPaths))]
        public void TestGetShortestPath_Find_Yes(
            IGraph graph, (int from, int to)[] sPath)
        {
            // Get shortest path
            IEnumerable<IConnection> sPathToTest =
                Dijkstra.GetShortestPath(
                    graph, sPath[0].from, sPath[sPath.Length - 1].to);

            // Check if actual shortest path was found
            Assert.Equal(
                sPath, sPathToTest.Select((c) => (c.FromNode, c.ToNode)));
        }
    }
}
