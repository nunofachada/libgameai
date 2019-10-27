namespace LibGameAI.PathFinding
{
    public class Connection : IConnection
    {
        public float Cost { get; }
        public int FromNode { get; }
        public int ToNode { get; }

        public Connection(float cost, int fromNode, int toNode)
        {
            Cost = cost;
            FromNode = fromNode;
            ToNode = toNode;
        }
    }
}
