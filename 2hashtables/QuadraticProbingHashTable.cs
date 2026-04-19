namespace _2hashtables;

public class QuadraticProbingHashTable : IMyHashTable
{
    private string?[] _keys;
    private int[] _values;
    private bool[] _deleted;
    private int _capacity;
    private int _count;
    private float _minThreshold;
    private float _maxThreshold;

    public QuadraticProbingHashTable(float minThreshold = 0.25F, float maxThreshold = 0.75F)
    {
        _capacity = 4;
        _keys = new string?[_capacity];
        _values = new int[_capacity];
        _deleted = new bool[_capacity];
        _count = 0;
        _minThreshold = minThreshold;
        _maxThreshold = maxThreshold;
    }

    /// <summary>
    /// Linearly scans the array from the index of the HashCode and forth,
    /// keeping track of the first avaliable square as it goes. If a null
    /// index is hit, we know we've never gone further so an already existing
    /// equal item cannot exist. If a deleted index is hit, it is possible that
    /// we at some point have added the same current item further on down the array,
    /// and we must keep track of the first avaliable index and keep searching.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void Add(string key, int value)
    {
        int index = HashCode(key);
        int startIndex = index;
        int firstTombstone = -1;
        int q = 1;

        while (_keys[index] != null)
        {
            if (_keys[index] == key && !_deleted[index])
            {
                _values[index] = value;
                return;
            }
            if (_deleted[index] && firstTombstone == -1)
            {
                firstTombstone = index;
            }

            index = (index + q * q++) % _capacity;
            if (index == startIndex) break;
        }

        if (firstTombstone != -1)
        {
            index = firstTombstone;
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
        int startIndex = index;
        int q = 1;

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
            index = (index + q * q++) % _capacity;
            if (index == startIndex) break;
        }
    }

    public int Get(string key)
    {
        int index = HashCode(key);
        int startIndex = index;
        int q = 1;

        while (_keys[index] != null)
        {
            if (_keys[index] == key && !_deleted[index])
            {
                return _values[index];
            }
            index = (index + q * q++) % _capacity;
            if (index == startIndex) break;
        }

        return -1;
    }

    public int Count => _count;
    public int Capacity => _capacity;

    /// <summary>
    /// Resize the HashMap based on if the current usage is above or below the pre-determined thresholds.
    /// If it's above, all data is transfered to an array of twice the size. If below, the new array is
    /// half the size.
    /// Items are added to the new array like normally, except we know there are no deleted squares, nor
    /// duplicates, so we always just place it at the first 'null' we encounter.
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
            if (_keys[i] == null || _deleted[i]) continue;

            int index = HashCode(_keys[i]!);
            int q = 1;

            while (newKeys[index] != null)
            {
                index = (index + q * q++) % _capacity;
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