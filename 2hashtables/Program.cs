using _2hashtables;
using System.Diagnostics;

var stopwatch = new Stopwatch();
stopwatch.Start();

var hashTable = new TriangularProbingHashTable(maxThreshold: 0.75f);

using var reader = new StreamReader(Console.OpenStandardInput());
int i = 0;
string? line;

while ((line = reader.ReadLine()) != null)
{
    string word = line.Trim();

    int currentValue = hashTable.Get(word);
    bool isPresent = currentValue != -1;
    bool removeIt = i % 16 == 0;

    if (isPresent)
    {
        if (removeIt) hashTable.Remove(word);
        else hashTable.Add(word, currentValue + 1);
    }
    else if (!removeIt)
    {
        hashTable.Add(word, 1);
    }

    i++;
}

stopwatch.Stop();

if (hashTable.Count > 0)
{
    string? bestWord = null;
    int maxCount = -1;

    foreach (var kvp in hashTable.GetAll())
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

Console.Error.WriteLine($"Time elapsed: {stopwatch.ElapsedMilliseconds} ms");
