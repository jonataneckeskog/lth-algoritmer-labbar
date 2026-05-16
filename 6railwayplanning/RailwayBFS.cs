namespace _6railwayplanning;

public static class RailwayBFS
{
    public static List<TEdge> FindShortestPath<TNode, TEdge>(
        Func<TNode, IEnumerable<TEdge>> getAvailableEdges,
        Func<TEdge, TNode, TNode> getOtherNode,
        TNode start,
        TNode end)
        where TNode : notnull, IEquatable<TNode>
    {
        if (start.Equals(end)) return [];

        Queue<TNode> queue = new();
        Dictionary<TNode, TEdge> edgeTo = new();

        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            TNode current = queue.Dequeue();

            if (current.Equals(end))
            {
                // Backtrack to build the path of edges
                List<TEdge> path = new();
                TNode backtrack = end;

                while (!backtrack.Equals(start))
                {
                    TEdge edgeUsed = edgeTo[backtrack];
                    path.Add(edgeUsed);

                    // Move backwards along the edge to the previous node
                    backtrack = getOtherNode(edgeUsed, backtrack);
                }

                path.Reverse();
                return path;
            }

            // Iterate over edges
            foreach (TEdge edge in getAvailableEdges(current))
            {
                TNode neighbor = getOtherNode(edge, current);

                if (!edgeTo.ContainsKey(neighbor) && !neighbor.Equals(start))
                {
                    edgeTo.Add(neighbor, edge);
                    queue.Enqueue(neighbor);
                }
            }
        }

        // No valid path found
        return [];
    }
}
