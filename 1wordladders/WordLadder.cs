namespace _1wordLadders;

public class WordLadder
{
    private readonly Dictionary<string, IReadOnlySet<string>> _wordGraph;

    public IReadOnlyDictionary<string, IReadOnlySet<string>> WordGraph => _wordGraph;

    public WordLadder()
    {
        _wordGraph = new Dictionary<string, IReadOnlySet<string>>();
    }

    public void InsertWord(string word)
    {
        throw new NotImplementedException("TODO");
    }
}

