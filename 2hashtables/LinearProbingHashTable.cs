namespace _2hashtables;

public class LinearProbingHashTable : IMyHashTable
{
    private string?[] _keys;
    private int[] _values;
    private bool[] _deleted;
    private int _capacity;
    private int _count;
    private float _minThreshold;
    private float _maxThreshold;

    public LinearProbingHashTable(float minThreshold = 0.25F, float maxThreshold = 0.75F)
    {
        _capacity = 4;
        _keys = new string?[_capacity];
        _values = new int[_capacity];
        _deleted = new bool[_capacity];
        _count = 0;
        _minThreshold = minThreshold;
        _maxThreshold = maxThreshold;
    }

    public void Add(string key, int value)
    {
        int index = HashCode(key);

        while (_keys[index] != null && !_deleted[index])
        {
            if (_keys[index] == key)
            {
                _values[index] = value;
                return;
            }

            index = (index + 1) % _capacity;
        }

        _keys[index] = key;
        _values[index] = value;
        _deleted[index] = false;
        _count++;

        Resize();
    }

    public void Remove(string key)
    {
        int index = HashCode(key);

        while (_keys[index] != null)
        {
            if (_keys[index] == key && !_deleted[index])
            {
                _deleted[index] = true;
                _values[index] = 0;
                _count--;
                Resize();
                return;
            }
            index = (index + 1) % _capacity;
        }
    }

    public int Get(string key)
    {
        int index = HashCode(key);

        while (_keys[index] != null)
        {
            if (_keys[index] == key && !_deleted[index])
            {
                return _values[index];
            }
            index = (index + 1) % _capacity;
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
        var newKeys = new string?[_capacity];
        var newValues = new int[_capacity];

        for (int i = 0; i < _keys.Length; i++)
        {
            if (_keys[i] == null) continue;

            int index = HashCode(_keys[i]!);

            while (newKeys[index] != null)
            {
                index = (index + 1) % _capacity;
            }

            newKeys[index] = _keys[i];
            newValues[index] = _values[i];
        }

        _keys = newKeys;
        _values = newValues;
        _deleted = new bool[_capacity];
    }

    private int HashCode(string key)
    {
        return (key.GetHashCode() & 0x7FFFFFFF) % _capacity;
    }

    public IEnumerable<(string Key, int Value)> GetAll()
    {
        for (int i = 0; i < _keys.Length; i++)
        {
            // Yield the key/value only if it's populated and not a tombstone
            if (_keys[i] != null && !_deleted[i])
            {
                yield return (_keys[i]!, _values[i]);
            }
        }
    }
}