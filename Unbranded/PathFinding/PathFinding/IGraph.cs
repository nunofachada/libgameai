using System;
using System.Collections.Generic;

namespace PathFinding
{
    public interface IGraph
    {
        int NumberOfNodes { get;  }
        IEnumerable<IConnection> GetConnections(int fromNode);
    }
}
