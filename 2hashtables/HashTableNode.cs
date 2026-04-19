namespace _2hashtables;

public class HashTableNode
{
    public string Key { get; set; }
    public int Value { get; set; }
    public HashTableNode? Next { get; set; }

    public HashTableNode(string key, int value, HashTableNode? next)
    {
        Key = key;
        Value = value;
        Next = next;
    }
}
