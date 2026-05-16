using _6railwayplanning;

///
/// LOAD DATA
/// 

var scanner = new Scanner(Console.OpenStandardInput());

int NodeCount = scanner.NextInt();  // N
int EdgeCount = scanner.NextInt();  // M
int C = scanner.NextInt();          // C
int RouteCount = scanner.NextInt(); // P

List<Railway> edges = scanner
    .AsEnumerable()
    .Chunk(3)
    .Take(EdgeCount)
    .Select(chunk => new Railway
    {
        StationU = chunk[0],
        StationV = chunk[1],
        Capacity = chunk[2]
    })
    .ToList();

List<StationId> routesToRemove = scanner
    .AsEnumerable()
    .Take(RouteCount)
    .Select(x => (StationId)x)
    .ToList();


///
/// SOLVE
///




///
/// OUTPUT
/// 

Console.WriteLine("Hello world!");
