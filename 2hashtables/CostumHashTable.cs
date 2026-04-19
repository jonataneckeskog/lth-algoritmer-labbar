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

    public void Add(string key, int value)
    {
        AddInternal(new HashNode(key, value, null), _data);
        _count++;
        Resize();
    }

    public void Remove(string key)
    {
        throw new NotImplementedException();
    }

    public int Get(string key)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Resize the HashMap based on if the current usage is above or below the pre-determined thresholds.
    /// If it's above, all data is transfered to an array of twice the size. If below, the new array is
    /// half the size.
    /// </summary>
    private void Resize()
    {
        float currentUsage = _count / _capacity;

        if (currentUsage < _minThreshold)
        {
            _capacity /= 2;

        }
        else if (currentUsage > _maxThreshold)
        {
            _capacity *= 2;
        }
        else return;

        // Transfer data to a new array (ReHash).
        HashNode[] newData = new HashNode[_capacity];

        foreach (var node in _data)
        {
            AddInternal(node, newData);
        }

        _data = newData;
    }

    private int HashCode(string key)
    {
        return (key.GetHashCode() & 0x7FFFFFFF) % _capacity;
    }

    private void AddInternal(HashNode node, HashNode[] targetArray)
    {
        throw new NotImplementedException();
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
