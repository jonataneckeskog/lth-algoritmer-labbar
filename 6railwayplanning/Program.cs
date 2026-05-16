using _6railwayplanning;

///
/// LOAD DATA
/// 

var scanner = new Scanner(Console.OpenStandardInput());

int NodeCount = scanner.NextInt();  // N
int EdgeCount = scanner.NextInt();  // M
int C = scanner.NextInt();          // C
int RouteCount = scanner.NextInt(); // P

Edge[] edges = scanner
    .AsEnumerable()
    .Chunk(3)
    .Take(EdgeCount)
    .Select(chunk => new Edge
    {
        StationU = chunk[0],
        StationV = chunk[1],
        Capacity = chunk[2]
    })
    .ToArray();

int[] routesToRemove = scanner
    .AsEnumerable()
    .Take(RouteCount)
    .ToArray();


///
/// SOLVE
///




///
/// OUTPUT
/// 

Console.WriteLine("Hello world!");
