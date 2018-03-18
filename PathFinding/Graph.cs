using System;
using System.Collections.Generic;

namespace PathFinding
{
    public class Graph : IGraph
    {

        private IConnection[][] connections;
        private int numberOfNodes;

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

        public Graph(IConnection[][] connections)
        {
            this.connections = connections;
        }

        public IEnumerable<IConnection> GetConnections(int fromNode)
        {
            return connections[fromNode];
        }

        public IEnumerable<IConnection> PathfindDijkstra(int start, int goal)
        {

            int current; 
            List<NodeRecord> open, closed;
            NodeRecord[] nodeRecords = new NodeRecord[numberOfNodes];

            // Initialize the record for the start node
            nodeRecords[start] = new NodeRecord(start);

            // There is no "current" node, set it to -1
            current = -1; 

            // Initialize the open and closed lists
            open = new List<NodeRecord>() { nodeRecords[start] };
            closed = new List<NodeRecord>();

            // Iterate through processing each node
            while (open.Count > 0)
            {

                // Find the smallest element in the open list
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
                    Func<NodeRecord, bool> findNode = 
                        nr => nr.Node == conn.ToNode;
                    Predicate<NodeRecord> fnp = 
                        new Predicate<NodeRecord>(findNode);

                    // Get cost estimate for the "to node"
                    float toNodeCost =
                        nodeRecords[current].CostSoFar + conn.Cost;

                    // Skip if the node is closed
                    nrIndex = closed.FindIndex(fnp);
                    if (nrIndex >= 0) continue;

                    // If node is open...
                    nrIndex = open.FindIndex(fnp);
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
