namespace _2hashtables;

public class SeparateChainingHashTable : IMyHashTable
{
    private HashTableNode?[] _data;
    private int _capacity;
    private int _count;
    private float _minThreshold;
    private float _maxThreshold;

    public SeparateChainingHashTable()
    {
        _capacity = 4;
        _data = new HashTableNode?[_capacity];
        _count = 0;
        _minThreshold = 0.25F;
        _maxThreshold = 0.75F;
    }

    public void Add(string key, int value)
    {
        int index = HashCode(key);
        var searchItem = _data[index];

        while (searchItem != null)
        {
            if (searchItem.Key == key)
            {
                searchItem.Value = value;
                return;
            }
            searchItem = searchItem.Next;
        }

        var newNode = new HashTableNode(key, value, _data[index]);
        _data[index] = newNode;
        _count++;

        Resize();
    }

    public void Remove(string key)
    {
        int index = HashCode(key);
        var searchItemCurrent = _data[index];

        if (searchItemCurrent == null) return;
        if (searchItemCurrent.Key == key)
        {
            _data[index] = searchItemCurrent.Next;
            _count--;
            Resize();
            return;
        }

        var searchItemNext = searchItemCurrent.Next;

        while (searchItemNext != null)
        {
            if (searchItemNext.Key == key)
            {
                searchItemCurrent.Next = searchItemNext.Next;
                _count--;
                Resize();
                return;
            }
            searchItemCurrent = searchItemNext;
            searchItemNext = searchItemNext.Next;
        }
    }

    public int Get(string key)
    {
        var item = _data[HashCode(key)];

        while (item != null)
        {
            if (item.Key == key) return item.Value;
            item = item.Next;
        }
        return -1;
    }

    public int Count => _count;
    public int Capacity => _capacity;

    /// <summary>
    /// Resize the HashMap based on if the current usage is above or below the pre-determined thresholds.
    /// If it's above, all data is transfered to an array of twice the size. If below, the new array is
    /// half the size.
    /// </summary>
    private void Resize()
    {
        float currentUsage = (float)_count / _capacity;

        if (currentUsage <= _minThreshold && _capacity > 4)
        {
            _capacity /= 2;

        }
        else if (currentUsage >= _maxThreshold)
        {
            _capacity *= 2;
        }
        else return;

        // Transfer data to a new array (ReHash).
        HashTableNode?[] newData = new HashTableNode?[_capacity];

        foreach (var headNode in _data)
        {
            var current = headNode;

            while (current != null)
            {
                var nextTemp = current.Next;

                int newIndex = HashCode(current.Key);

                current.Next = newData[newIndex];
                newData[newIndex] = current;

                current = nextTemp;
            }
        }

        _data = newData;
    }

    private int HashCode(string key)
    {
        return (key.GetHashCode() & 0x7FFFFFFF) % _capacity;
    }

    public IEnumerable<(string Key, int Value)> GetAll()
    {
        for (int i = 0; i < _data.Length; i++)
        {
            var current = _data[i];

            while (current != null)
            {
                yield return (current.Key, current.Value);
                current = current.Next;
            }
        }
    }
}
