using _1wordLadders;

var wordLadder = new WordLadder();

// Read first line: N M
string? firstLine = Console.ReadLine();
if (firstLine == null) return;

var parts = firstLine.Split(' ');
int N = int.Parse(parts[0]);
int M = int.Parse(parts[1]);

// Read N words
for (int i = 0; i < N; i++)
{
    string? word = Console.ReadLine();
    if (word != null)
    {
        wordLadder.InsertWord(word.Trim());
    }
}

// Read M queries and process them
for (int i = 0; i < M; i++)
{
    string? queryLine = Console.ReadLine();
    if (queryLine != null)
    {
        var queryParts = queryLine.Split(' ');
        string start = queryParts[0];
        string end = queryParts[1];

        int distance = PathSearch.ShortestPath(wordLadder, start, end);
        if (distance == -1)
        {
            Console.WriteLine("Impossible");
        }
        else
        {
            Console.WriteLine(distance);
        }
    }
}
