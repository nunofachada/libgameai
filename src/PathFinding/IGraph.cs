using System;
using System.Collections.Generic;

namespace LibGameAI.PathFinding
{
    public interface IGraph
    {
        int NumberOfNodes { get;  }
        IEnumerable<IConnection> GetConnections(int fromNode);
    }
}
