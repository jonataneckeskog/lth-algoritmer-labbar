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

    [Fact]
    public void EmptyTable_HasCorrectSizes()
    {
        Assert.Equal(0, _hashTable.Count);
        Assert.Equal(4, _hashTable.Capacity);
    }

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

    [Fact]
    public void RemoveNonExistentItem_DoesNothing()
    {
        _hashTable.Remove("XXX");
        Assert.Equal(0, _hashTable.Count);
        Assert.Equal(4, _hashTable.Capacity);
    }

    [Fact]
    public void AddMultipleItems_ToEmptyTable_IncreasesCapacityByFactorOfTwo()
    {
        _hashTable.Add("apple", 1);
        _hashTable.Add("banana", 2); // 50% capacity
        _hashTable.Add("cherry", 3); // 75% capacity, resizes now
        Assert.Equal(8, _hashTable.Capacity);
    }

    [Fact]
    public void RemoveItem_FromLargeTable_DecreasesSize()
    {
        _hashTable.Add("apple", 1);
        _hashTable.Add("banana", 2);
        _hashTable.Add("cherry", 3);
        _hashTable.Add("kiwi", 4); // 100% capacity, should have resized to 8 already
        _hashTable.Remove("apple"); // Down to 75%
        _hashTable.Remove("banana"); // Down to 50%
        _hashTable.Remove("cherry"); // Down to 25%, resizes back to 4 now
        Assert.Equal(4, _hashTable.Capacity);
    }

    [Fact]
    public void Get_NonExistentKey_ReturnsMinusOne()
    {
        var table = CreateHashTable();
        Assert.Equal(-1, table.Get("ghost"));
    }
}
