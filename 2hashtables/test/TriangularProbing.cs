namespace _2hashtables.Tests;

public class TriangularProbing : HashTableTestBase
{
    public override IMyHashTable CreateHashTable()
    {
        return new TriangularProbingHashTable();
    }
}
