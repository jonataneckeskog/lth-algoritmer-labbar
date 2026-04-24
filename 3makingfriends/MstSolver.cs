namespace _3makingfriends;

public class MstSolver : ITimeMinimizer
{
    private readonly int _personCount;
    private readonly Edge[] _edges;
    private readonly int[] people;

    /// <summary>
    /// Takes possesion of the edges array and will mutate it. The caller
    /// shouldn't use it any longer.
    /// </summary>
    public MstSolver(int personCount, Edge[] edges)
    {
        _personCount = personCount;
        _edges = edges;
        people = new int[_personCount + 1];
        for (int i = 0; i <= _personCount; i++)
        {
            people[i] = i;
        }
    }

    public long CalculateMinimumTime()
    {
        _edges.Sort((a, b) => a.TimeToConnect - b.TimeToConnect);

        long totalTime = 0;

        foreach (var edge in _edges)
        {
            int rootA = FindRoot(edge.PersonU);
            int rootB = FindRoot(edge.PersonV);

            // Not connected, add the connection and increase time
            if (rootA != rootB)
            {
                people[rootA] = rootB;
                totalTime += edge.TimeToConnect;
            }
        }

        return totalTime;
    }

    private int FindRoot(int person)
    {
        if (people[person] == person) return person;
        return people[person] = FindRoot(people[person]);
    }
}
