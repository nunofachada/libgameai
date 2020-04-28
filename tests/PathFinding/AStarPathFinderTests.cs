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
    public class AStarPathFinderTests
    {
        // Returns a collection of (graph, heuristics, start, end) tuples
        // with at least a path between start and end nodes
        public static TheoryData<IGraph, float[], int, int, bool> GraphsWithPath
        {
            get
            {
                TheoryData<IGraph, float[], int, int, bool> data =
                    new TheoryData<IGraph, float[], int, int, bool>();

                (IGraph, float[], int, int)[] graphData =
                    new (IGraph, float[], int, int)[]
                {
                    (
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
                    // Heuristics
                    new float[]
                        { 7.0f, 6.0f, 3.0f, 5.5f, 4.1f, 0.2f, 2.0f, 0.0f },
                    0, // Start node
                    7  // End node
                    ),
                    (
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
                    // Heuristics
                    new float[] { 4.0f, 3.0f, 2.4f, 3.3f, 2.3f, 1.1f, 0.0f },
                    0, // Start node
                    6  // End node
                    ),
                    (
                    // Graph represented by an adjacency matrix
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
                            new IConnection[] { new Connection(5f, 2, 5) },
                            new IConnection[] { new Connection(3f, 3, 4) },
                            new IConnection[] { new Connection(2f, 4, 5) },
                            new IConnection[] { }
                        }
                    ),
                    // Heuristics
                    new float[] { 6f, 2f, 3f, 1f, 1f, 0f },
                    0, // Start node
                    5  // End node
                    ),
                    (
                    // Graph represented by an adjacency matrix
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
                            new IConnection[] { },
                            new IConnection[] { new Connection(1.4f, 4, 6) },
                            new IConnection[] { },
                            new IConnection[] { }
                        }
                    ),
                    // Heuristics
                    new float[] { 4.2f, 3.2f, 3.7f, 2.8f, 1.6f, 1.4f, 0.0f },
                    0, // Start node
                    6  // End node
                    )
                };

                foreach ((IGraph, float[], int, int) gd in graphData)
                {
                    // With early exit
                    data.Add(gd.Item1, gd.Item2, gd.Item3, gd.Item4, true);
                    // Without early exit
                    data.Add(gd.Item1, gd.Item2, gd.Item3, gd.Item4, false);
                }

                return data;
            }
        }

        // Returns a collection of (graph, start, end) tuples without a path
        // between start and end nodes
        public static TheoryData<IGraph, float[], int, int, bool> GraphsNoPath
        {
            get
            {
                TheoryData<IGraph, float[], int, int, bool> data =
                    new TheoryData<IGraph, float[], int, int, bool>();

                (IGraph, float[], int, int)[] graphData =
                    new (IGraph, float[], int, int)[]
                {
                    (
                    new Graph(new float[,]
                    {
                        { 0.0f, 1.5f, 0.0f },
                        { 0.5f, 0.0f, 0.0f },
                        { 2.1f, 1.7f, 0.0f }
                    }),
                    // Heuristics
                    new float[] { 3f, 2f, 0f },
                    0, // Start node
                    2  // End node
                    ),
                    (
                    // Graph represented by an adjacency matrix
                    new Graph(
                        new IConnection[][]
                        {
                            new IConnection[] { new Connection(1.5f, 0, 1) },
                            new IConnection[] { new Connection(0.5f, 1, 0) },
                            new IConnection[]
                            {
                                new Connection(2.1f, 2, 0),
                                new Connection(1.7f, 2, 1)
                            }
                        }
                    ),
                    // Heuristics
                    new float[] { 3f, 2f, 0f },
                    0, // Start node
                    2  // End node
                    )
                };

                foreach ((IGraph, float[], int, int) gd in graphData)
                {
                    // With early exit
                    data.Add(gd.Item1, gd.Item2, gd.Item3, gd.Item4, true);
                    // Without early exit
                    data.Add(gd.Item1, gd.Item2, gd.Item3, gd.Item4, false);
                }

                return data;
            }
        }

        // Check if a path is found with A* algorithm
        [Theory]
        [MemberData(nameof(GraphsWithPath))]
        public void TestFindPath_Find_Yes(
            IGraph graph, float[] heuristics, int from, int to, bool earlyExit)
        {
            // Instantiate an A* path finder
            IPathFinder pathFinder =
                new AStarPathFinder((i) => heuristics[i], earlyExit);

            // Get a path
            IEnumerable<IConnection> sPathToTest =
                pathFinder.FindPath(graph, from, to);

            // Check if a path was found
            Assert.Equal(
                (from, to),
                (sPathToTest.First().FromNode, sPathToTest.Last().ToNode));
        }

        // Check if a path is not found with A* algorithm
        [Theory]
        [MemberData(nameof(GraphsNoPath))]
        public void TestFindPath_Find_No(
            IGraph graph, float[] heuristics, int from, int to, bool earlyExit)
        {
            // Instantiate an A* path finder
            IPathFinder pathFinder =
                new AStarPathFinder((i) => heuristics[i], earlyExit);

            // Get a path
            IEnumerable<IConnection> sPathToTest =
                pathFinder.FindPath(graph, from, to);

            // Check if a path was found
            Assert.Null(sPathToTest);
        }
    }
}
