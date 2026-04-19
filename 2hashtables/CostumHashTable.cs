namespace _2hashtables;

public class CostomHashTable
{
    private string[] _data;
    private int _capacity;
    private int _count;
    private float _minThreshold;
    private float _maxThreshold;


    public CostomHashTable()
    {
        _capacity = 4;
        _data = new string[_capacity];
        _count = 0;
        _minThreshold = 0.25F;
        _maxThreshold = 0.75F;
    }

    private class HashNode
    {

    }
}
