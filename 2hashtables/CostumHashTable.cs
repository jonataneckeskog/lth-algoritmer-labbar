namespace _2hashtables;

public class CostomHashTable
{
    private HashNode[] _data;
    private int _capacity;
    private int _count;
    private float _minThreshold;
    private float _maxThreshold;

    public CostomHashTable()
    {
        _capacity = 4;
        _data = new HashNode[_capacity];
        _count = 0;
        _minThreshold = 0.25F;
        _maxThreshold = 0.75F;
    }

    private class HashNode
    {
        public string Key { get; set; }
        public int Value { get; set; }
        public HashNode? Next { get; set; }

        public HashNode(string key, int value, HashNode? next)
        {
            Key = key;
            Value = value;
            Next = next;
        }
    }
}
