namespace _6railwayplanning;

public static class RailwayBFS
{
    public static List<T> FindShortestPath<T>(Func<T, IEnumerable<T>> getRailways, T start, T end)
        where T : notnull, IEquatable<T>
    {
        if (start.Equals(end)) return [];

        Queue<T> queue = [];
        Dictionary<T, T> parentEdges = [];

        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            T current = queue.Dequeue();

            if (current.Equals(end))
            {
                // Backtrack to build the path
                List<T> path = [];
                T backtrack = end;

                while (!backtrack.Equals(start))
                {
                    T edgeUsed = parentEdges[backtrack];
                    path.Add(edgeUsed);

                    backtrack = parentEdges[edgeUsed];
                }

                path.Reverse();
                return path;
            }

            foreach (T neighbor in getRailways(current))
            {
                if (!parentEdges.ContainsKey(neighbor) && !neighbor.Equals(start))
                {
                    parentEdges.Add(neighbor, current);
                    queue.Enqueue(neighbor);
                }
            }
        }

        // No valid path with available capacity was found
        return [];
    }
}
