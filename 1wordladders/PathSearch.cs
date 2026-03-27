namespace _1wordLadders;

public static class PathSearch
{
    /// <summary>
    /// Implementation of BFS, using WordLadder as the data structure. ShortestPath
    /// returns the length of the path from startWord to endWord. If the path isn't found,
    /// -1 is returned.
    /// </summary>
    public static int ShortestPath(WordLadder wordLadder, string startWord, string endWord)
    {
        if (startWord == endWord) return 0;

        Queue<string> queue = new Queue<string>();
        Dictionary<string, int> distances = new Dictionary<string, int>();

        queue.Enqueue(startWord);
        distances.Add(startWord, 0);

        while (queue.Count > 0)
        {
            string currentWord = queue.Dequeue();

            // Return the recorded distance if endWord is found
            if (currentWord == endWord)
            {
                return distances[currentWord];
            }

            // Look up the neighbors
            if (wordLadder.WordGraph.TryGetValue(currentWord, out var neighbors))
            {
                foreach (string neighbor in neighbors)
                {
                    // Only process nodes not visited yet
                    if (!distances.ContainsKey(neighbor))
                    {
                        // The distance is the current word's distance + 1
                        distances.Add(neighbor, distances[currentWord] + 1);

                        // Add the neighbor to the queue to be checked later
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        // No path found
        return -1;
    }
}
