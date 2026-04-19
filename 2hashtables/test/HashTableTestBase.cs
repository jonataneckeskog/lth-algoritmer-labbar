using Xunit;

namespace _2hashtables.Tests;

public abstract class HashTableTestBase
{
    private IMyHashTable _hashTable;

    public HashTableTestBase()
    {
        _hashTable = CreateHashTable();
    }

    public abstract IMyHashTable CreateHashTable();

    [Theory]
    [InlineData("apple", 1)]
    [InlineData("banana", 2)]
    public void AddItem_ToEmptyTable_IncreasesSize(string key, int value)
    {
        _hashTable.Add(key, value);
        Assert.Equal(1, _hashTable.Count);
    }

    [Theory]
    [InlineData("apple", 1)]
    [InlineData("banana", 2)]
    public void AddItem_ToNonEmptyTable_IncreasesSize(string key, int value)
    {
        _hashTable.Add("XXX", 42);
        _hashTable.Add(key, value);
        Assert.Equal(2, _hashTable.Count);
    }
}
