using _3makingfriends;

var scanner = new Scanner(Console.OpenStandardInput());

int N = scanner.NextInt();
int M = scanner.NextInt();

Edge[] allConnections = new Edge[M];

for (int i = 0; i < M; i++)
{
    allConnections[i] = new Edge
    {
        PersonU = scanner.NextInt(),
        PersonV = scanner.NextInt(),
        TimeToConnect = scanner.NextInt()
    };
}
