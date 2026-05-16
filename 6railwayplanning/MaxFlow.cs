namespace _6railwayplanning;

public static class MaxFlow
{
    public static int CalculateMaxFlow(List<NetworkEdge>[] adjList, int initialFlow, int source, int sink)
    {
        int totalFlow = initialFlow;

        while (true)
        {
            // Find an shortest valid path using BFS
            List<NetworkEdge> path = RailwayBFS.FindShortestPath<StationId, NetworkEdge>(
                current => adjList[current.Id].Where(edge => edge.GetResidualCapacity(current) > 0),
                // Given an edge and the current node, get the node on the other side
                (edge, current) => edge.GetOtherStation(current),
                source,
                sink
            );

            // If no path is found, we're done
            if (path.Count == 0) break;

            // Find the bottleneck value along this path
            int bottleneck = int.MaxValue;
            int currentNode = source;
            foreach (var edge in path)
            {
                int residual = edge.GetResidualCapacity(currentNode);
                if (residual < bottleneck) bottleneck = residual;

                // Move to the next station in the chain
                currentNode = (currentNode == edge.U) ? edge.V : edge.U;
            }

            // Push the bottleneck flow through the path
            currentNode = source;
            foreach (var edge in path)
            {
                edge.ApplyFlow(currentNode, bottleneck);
                currentNode = (currentNode == edge.U) ? edge.V : edge.U;
            }

            // Add to the running total
            totalFlow += bottleneck;
        }

        return totalFlow;
    }
}