using System;
using System.Collections.Generic;

namespace PathFinding
{
    public class Graph : IGraph
    {
        // The graph is internally represented using an adjacency list
        private IEnumerable<IConnection>[] connections;

        /// <summary>
        /// This private class is used to keep node records for the path
        /// finding algorithms.
        /// </summary>
        private class NodeRecord : IComparable<NodeRecord>
        {
            public int Node { get; }
            public IConnection Connection { get; set; }
            public float CostSoFar { get; set; }

            public NodeRecord(int node)
            {
                this.Node = node;
                this.Connection = null;
                this.CostSoFar = 0.0f;
            }

            public int CompareTo(NodeRecord other)
            {
                return Math.Sign(CostSoFar - other.CostSoFar);
            }
        }

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

        /// <summary>
        /// Find shortest path between start and goal nodes.
        /// </summary>
        /// <param name="start">Start node.</param>
        /// <param name="goal">Goal node.</param>
        /// <returns>
        /// An enumerable containing the connections that constitute
        /// the shortest path from start to goal.
        /// </returns>
        public IEnumerable<IConnection> PathfindDijkstra(int start, int goal)
        {

            int current;
            List<NodeRecord> open, closed;
            NodeRecord[] nodeRecords = new NodeRecord[NumberOfNodes];

            // Initialize the record for the start node
            nodeRecords[start] = new NodeRecord(start);

            // "Current" node is start node
            current = start;

            // Initialize the open and closed lists
            open = new List<NodeRecord>() { nodeRecords[start] };
            closed = new List<NodeRecord>();

            // Iterate through processing each node
            while (open.Count > 0)
            {

                // Find element with smallest cost so far in the open list
                open.Sort();
                current = open[0].Node;

                // If it is end node, break out of node processing loop
                if (current == goal) break;

                // Otherwise get the node outgoing connections
                foreach (IConnection conn in GetConnections(current))
                {
                    // Index of node record in the open and closed lists
                    int nrIndex;

                    // The node record itself
                    NodeRecord nodeRec;

                    // Function to find specific node in a list
                    Predicate<NodeRecord> findNodePred =
                        new Predicate<NodeRecord>(nr => nr.Node == conn.ToNode);

                    // Get cost estimate for the "to node"
                    float toNodeCost =
                        nodeRecords[current].CostSoFar + conn.Cost;

                    // Skip if the node is closed
                    nrIndex = closed.FindIndex(findNodePred);
                    if (nrIndex >= 0) continue;

                    // If node is open...
                    nrIndex = open.FindIndex(findNodePred);
                    if (nrIndex >= 0)
                    {
                        // ...and we find a worse route, also skip
                        if (open[nrIndex].CostSoFar <= toNodeCost) continue;

                        // Otherwise, keep node record
                        nodeRec = open[nrIndex];
                    }
                    else
                    {
                        // If we're here we've got an unvisited node, so make
                        // a record for it
                        nodeRec = new NodeRecord(conn.ToNode);
                        nodeRecords[conn.ToNode] = nodeRec;
                    }

                    // We're here if we need to update the node
                    // Update the cost and connection
                    nodeRec.CostSoFar = toNodeCost;
                    nodeRec.Connection = conn;

                    // And add it to the open list, if it's not already there
                    if (nrIndex < 0)
                    {
                        open.Add(nodeRec);
                    }
                }

                // We've finished looking at the connections for the current
                // node, so add it to the closed list and remove it from the
                // open list
                open.Remove(nodeRecords[current]);
                closed.Add(nodeRecords[current]);
            }

            // We're here if we've either found the goal, or if we've no more
            // nodes to search

            if (current != goal)
            {
                // We've run out of nodes without finding the goal, so there's
                // no solution
                return null;
            }
            else
            {
                // Compile the list of connections in the path
                LinkedList<IConnection> path = new LinkedList<IConnection>();

                // Work back along the path, accumulating connections
                while (current != start)
                {
                    path.AddFirst(nodeRecords[current].Connection);
                    current = nodeRecords[current].Connection.FromNode;
                }

                // Return the path
                return path;
            }
        }
    }
}
