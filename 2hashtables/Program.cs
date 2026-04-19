using _2hashtables;

// HashTable, currently just the standard library one
var hashTable = new Dictionary<string, int>();

// Read words from standard input
using var reader = new StreamReader(Console.OpenStandardInput());
int i = 0;
string? line;

while ((line = reader.ReadLine()) != null)
{
    string word = line.Trim();

    bool isPresent = hashTable.ContainsKey(word);
    bool removeIt = i % 16 == 0;

    if (isPresent)
    {
        if (removeIt) hashTable.Remove(word);
        else hashTable[word]++;
    }
    else if (!removeIt)
    {
        hashTable[word] = 1;
    }

    i++;
}

if (hashTable.Count > 0)
{
    string? bestWord = null;
    int maxCount = -1;

    foreach (var kvp in hashTable)
    {
        if (kvp.Value > maxCount)
        {
            maxCount = kvp.Value;
            bestWord = kvp.Key;
        }
        else if (kvp.Value == maxCount)
        {
            if (string.CompareOrdinal(kvp.Key, bestWord) < 0)
            {
                bestWord = kvp.Key;
            }
        }
    }

    Console.WriteLine($"{bestWord} {maxCount}");
}
