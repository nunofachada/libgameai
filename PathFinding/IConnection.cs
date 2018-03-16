namespace PathFinding
{
    public interface IConnection
    {
        float Cost { get; }
        int FromNode { get; }
        int ToNode { get; }
    }
}
