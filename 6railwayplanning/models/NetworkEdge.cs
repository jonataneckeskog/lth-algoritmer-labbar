namespace _6railwayplanning;

public class NetworkEdge
{
    public int U { get; }
    public int V { get; }
    public int Capacity { get; }
    public int Flow { get; set; } // Net flow from U to V

    public NetworkEdge(Railway raw)
    {
        U = raw.StationU;
        V = raw.StationV;
        Capacity = raw.Capacity;
        Flow = 0;
    }

    public int GetResidualCapacity(int fromNode)
    {
        return fromNode == U ? Capacity - Flow : Capacity + Flow;
    }

    public void ApplyFlow(int fromNode, int amount)
    {
        if (fromNode == U)
            Flow += amount; // Pushing forward
        else
            Flow -= amount; // Pushing backward (canceling)
    }
}