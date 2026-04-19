namespace _2hashtables.Tests;

public class QuadraticProbing : HashTableTestBase
{
    public override IMyHashTable CreateHashTable()
    {
        return new QuadraticProbingHashTable();
    }
}