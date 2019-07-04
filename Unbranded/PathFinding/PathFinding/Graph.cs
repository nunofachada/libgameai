﻿using System;
using System.Collections.Generic;

namespace PathFinding
{
    public class Graph : IGraph
    {
        // The graph is internally represented using an adjacency list
        private IEnumerable<IConnection>[] connections;


        /// <summary>
        /// Number of nodes in this graph.
        /// </summary>
        public int NumberOfNodes { get => connections.Length; }

        /// <summary>
        /// Create new graph using an adjacency list.
        /// </summary>
        /// <param name="connections">Adjacency list with which to create
        /// graph.</param>
        public Graph(IEnumerable<IConnection>[] connections)
        {
            this.connections = connections;
        }

        /// <summary>
        /// Create new graph using an adjacency matrix.
        /// </summary>
        /// <param name="adjMatrix">Adjacency matrix with which to create
        /// graph.</param>
        public Graph(float[,] adjMatrix)
        {
            // Adjacency matrix must be square
            if (adjMatrix.GetLength(0) != adjMatrix.GetLength(1))
            {
                throw new Exception("Adjacency matrix must be square!");
            }

            // Initialize the internal adjacency list which defines this graph
            connections = new IEnumerable<IConnection>[adjMatrix.GetLength(0)];

            // Cycle through the adjaceny matrix and build the internal
            // adjacency list
            for (int i = 0; i < adjMatrix.GetLength(0); i++)
            {
                // Create list for current node
                List<Connection> currList = new List<Connection>();

                // Populate list for current node
                for (int j = 0; j < adjMatrix.GetLength(1); j++)
                {
                    // Only add connections if weight/cost is larger than 0
                    if (adjMatrix[i, j] > 0.0f)
                    {
                        currList.Add(new Connection(adjMatrix[i, j], i, j));
                    }
                }

                // Keep current node's list in the adjacency list
                connections[i] = currList;
            }
        }

        /// <summary>
        /// Get all outgoing connections for the given node.
        /// </summary>
        /// <param name="fromNode">A node in the graph.</param>
        /// <returns>All outgoing connections for the given node.</returns>
        public IEnumerable<IConnection> GetConnections(int fromNode)
        {
            return connections[fromNode];
        }
    }
}