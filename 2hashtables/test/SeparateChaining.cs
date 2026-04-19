namespace _2hashtables.Tests;

public class SeparateChaining : HashTableTestBase
{
    public override IMyHashTable CreateHashTable()
    {
        return new SeparateChainingHashTable();
    }
}
