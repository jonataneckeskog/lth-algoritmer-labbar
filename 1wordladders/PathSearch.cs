namespace _1wordLadders;

public static class PathSearch
{
    public static int ShortestPath(WordLadder wordLadder, string startWord, string endWord)
    {
        Queue<string> wordsToCheck = new Queue<string>(wordLadder.WordGraph[startWord]);

        while (wordsToCheck.Count > 0)
        {
            string word = wordsToCheck.Dequeue();

            if (word == endWord)
            {
                return 0;
            }
        }

        return 0;
    }
}
