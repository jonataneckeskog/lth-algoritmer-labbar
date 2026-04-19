namespace _2hashtables;

public interface IMyHashTable
{
    void Add(string key, int value);
    void Remove(string key);
    int Get(string key);
    int Count { get; }
    int Capacity { get; }
    IEnumerable<(string Key, int Value)> GetAll();
}
