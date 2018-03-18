﻿using System;
using System.Collections.Generic;

namespace PathFinding
{
    public interface IGraph
    {
        IEnumerable<IConnection> GetConnections(int fromNode);
        IEnumerable<IConnection> PathfindDijkstra(int start, int goal);
    }
}
