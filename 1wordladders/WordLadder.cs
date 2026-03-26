namespace _1wordLadders;

public class WordLadder
{
    private readonly Dictionary<string, HashSet<string>> _wordGraph;

    public IReadOnlyDictionary<string, HashSet<string>> WordGraph => _wordGraph;

    public WordLadder()
    {
        _wordGraph = new Dictionary<string, HashSet<string>>();
    }

    /// <summary>
    /// Insert a word into the WordLadder data structure and wire up its edges
    /// </summary>
    public void InsertWord(string newWord)
    {
        var newWordConnections = new HashSet<string>();

        foreach (var entry in _wordGraph)
        {
            string existingWord = entry.Key;

            // Does the NEW word connect to the EXISTING word?
            if (CanConnect(newWord, existingWord))
            {
                newWordConnections.Add(existingWord);
            }

            // Does the EXISTING word connect to the NEW word?
            if (CanConnect(existingWord, newWord))
            {
                entry.Value.Add(newWord); // We add the newWord to the existing word's edge list!
            }
        }

        // Add the new word to the dictionary
        _wordGraph.Add(newWord, newWordConnections);
    }

    /// <summary>
    /// Checks if all of the last 4 letters of the source word exist in the target word.
    /// </summary>
    private bool CanConnect(string source, string target)
    {
        // Convert target to an array so we can "cross out" letters as we match them
        char[] targetChars = target.ToCharArray();

        // Loop through the last 4 letters of the source word (indices 1 through 4)
        for (int i = 1; i < 5; i++)
        {
            char letterToFind = source[i];
            bool matchFound = false;

            // Scan the target word for this letter
            for (int j = 0; j < 5; j++)
            {
                if (targetChars[j] == letterToFind)
                {
                    targetChars[j] = ' '; // Cross it out so it can't be reused!
                    matchFound = true;
                    break;
                }
            }

            // If we failed to find one of the required letters, the road is closed
            if (!matchFound) return false;
        }

        // If we survived the loop, all 4 letters were found
        return true;
    }
}