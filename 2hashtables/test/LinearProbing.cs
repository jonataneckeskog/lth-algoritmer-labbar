namespace _2hashtables.Tests;

public class LinearProbing : HashTableTestBase
{
    public override IMyHashTable CreateHashTable()
    {
        return new LinearProbingHashTable();
    }
}
