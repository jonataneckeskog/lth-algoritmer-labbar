namespace _2hashtables;

public class TriangularProbingHashTable : IMyHashTable
{
    private string?[] _keys;
    private int[] _values;
    private bool[] _deleted;
    private int _capacity;
    private int _count;
    private float _minThreshold;
    private float _maxThreshold;

    public TriangularProbingHashTable(float minThreshold = 0.25F, float maxThreshold = 0.75F)
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
        int startIndex = HashCode(key);
        int firstTombstone = -1;
        int offset = 0;

        for (int i = 0; i < _capacity; i++)
        {
            int index = (startIndex + offset) & (_capacity - 1);

            if (_keys[index] == null)
            {
                // Found an empty spot. Use it, or the first tombstone if we found one.
                int insertIndex = (firstTombstone != -1) ? firstTombstone : index;
                _keys[insertIndex] = key;
                _values[insertIndex] = value;
                _deleted[insertIndex] = false;
                _count++;
                Resize();
                return;
            }

            if (_keys[index] == key && !_deleted[index])
            {
                _values[index] = value;
                return;
            }

            if (_deleted[index] && firstTombstone == -1)
            {
                firstTombstone = index;
            }

            // Prepare for next probe using triangular number sequence i*(i+1)/2
            offset += (i + 1);
        }

        // This part is reached only if the table is full of items and tombstones.
        if (firstTombstone != -1)
        {
            _keys[firstTombstone] = key;
            _values[firstTombstone] = value;
            _deleted[firstTombstone] = false;
            _count++;
            Resize();
        }
        // If no tombstone, the table is 100% full, which should be prevented by resizing.
    }

    public void Remove(string key)
    {
        int startIndex = HashCode(key);
        int offset = 0;

        for (int i = 0; i < _capacity; i++)
        {
            int index = (startIndex + offset) & (_capacity - 1);
            if (_keys[index] == null) return;

            if (_keys[index] == key && !_deleted[index])
            {
                _deleted[index] = true;
                _values[index] = 0;
                _count--;
                Resize();
                return;
            }
            offset += i + 1;
        }
    }

    public int Get(string key)
    {
        int startIndex = HashCode(key);
        int offset = 0;

        for (int i = 0; i < _capacity; i++)
        {
            int index = (startIndex + offset) & (_capacity - 1);

            if (_keys[index] == null) return -1;
            if (_keys[index] == key && !_deleted[index]) return _values[index];

            offset += i + 1;
        }

        return -1;
    }

    public int Count => _count;
    public int Capacity => _capacity;

    private void Resize()
    {
        float currentUsage = (float)_count / _capacity;
        int oldCapacity = _capacity;

        if (currentUsage <= _minThreshold && _capacity > 4)
        {
            _capacity /= 2;
        }
        else if (currentUsage >= _maxThreshold)
        {
            _capacity *= 2;
        }
        else return;

        var newKeys = new string?[_capacity];
        var newValues = new int[_capacity];

        for (int i = 0; i < oldCapacity; i++)
        {
            if (_keys[i] == null || _deleted[i]) continue;

            int startIndex = HashCode(_keys[i]!);
            int offset = 0;

            for (int j = 0; j < _capacity; j++)
            {
                int index = (startIndex + offset) & (_capacity - 1);
                if (newKeys[index] == null)
                {
                    newKeys[index] = _keys[i];
                    newValues[index] = _values[i];
                    break;
                }
                offset += j + 1;
            }
        }

        _keys = newKeys;
        _values = newValues;
        _deleted = new bool[_capacity];
    }

    private int HashCode(string key)
    {
        return key.GetHashCode() & 0x7FFFFFFF & (_capacity - 1);
    }

    public IEnumerable<(string Key, int Value)> GetAll()
    {
        for (int i = 0; i < _keys.Length; i++)
        {
            if (_keys[i] != null && !_deleted[i])
            {
                yield return (_keys[i]!, _values[i]);
            }
        }
    }
}