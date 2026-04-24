using _3makingfriends;

///
/// LOAD DATA
/// 

var scanner = new Scanner(Console.OpenStandardInput());

int PersonCount = scanner.NextInt();
int EdgeCount = scanner.NextInt();

Edge[] allConnections = new Edge[EdgeCount];

for (int i = 0; i < EdgeCount; i++)
{
    allConnections[i] = new Edge
    {
        PersonU = scanner.NextInt(),
        PersonV = scanner.NextInt(),
        TimeToConnect = scanner.NextInt()
    };
}


///
/// SOLVE
/// 

ITimeMinimizer solver = new MstSolver(PersonCount, allConnections);
long time = solver.CalculateMinimumTime();


///
/// OUTPUT
/// 

Console.WriteLine(time);
