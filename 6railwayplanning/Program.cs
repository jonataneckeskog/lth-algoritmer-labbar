using _6railwayplanning;

///
/// LOAD DATA
/// 

var scanner = new Scanner(Console.OpenStandardInput());

int NodeCount = scanner.NextInt();  // N
int EdgeCount = scanner.NextInt();  // M
int C = scanner.NextInt();          // C
int RouteCount = scanner.NextInt(); // P

List<Railway> parsedEdges = scanner
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

var adjacencyList = new List<NetworkEdge>[NodeCount];

foreach (var rawEdge in parsedEdges)
{
    var edge = new NetworkEdge(rawEdge);
    adjacencyList[edge.U].Add(edge);
    adjacencyList[edge.V].Add(edge);
}

var maxFlow = MaxFlow.CalculateMaxFlow(adjacencyList, 0, NodeCount - 1);


///
/// OUTPUT
/// 

Console.WriteLine($"NBR_REMOVED {maxFlow}");
