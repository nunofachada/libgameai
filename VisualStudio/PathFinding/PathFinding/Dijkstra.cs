﻿using System;
using System.Collections.Generic;

namespace PathFinding
{
    public static class Dijkstra
    {

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
        /// Find shortest path between start and goal nodes.
        /// </summary>
        /// <param name="graph">Graph where to perform search.</param>
        /// <param name="start">Start node.</param>
        /// <param name="goal">Goal node.</param>
        /// <returns>
        /// An enumerable containing the connections that constitute
        /// the shortest path from start to goal.
        /// </returns>
        public static IEnumerable<IConnection> GetShortestPath(
            IGraph graph, int start, int goal)
        {

            int current;
            List<NodeRecord> open, closed;
            NodeRecord[] nodeRecords = new NodeRecord[graph.NumberOfNodes];

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
                foreach (IConnection conn in graph.GetConnections(current))
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

                        // And add it to the open list
                        open.Add(nodeRec);
                    }

                    // We're here if we need to update the node
                    // Update the cost and connection
                    nodeRec.CostSoFar = toNodeCost;
                    nodeRec.Connection = conn;
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