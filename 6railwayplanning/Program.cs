using _6railwayplanning;

///
/// LOAD DATA
/// 

var scanner = new Scanner(Console.OpenStandardInput());

int NodeCount = scanner.NextInt();  // N
int EdgeCount = scanner.NextInt();  // M
int C = scanner.NextInt();          // C
int RouteCount = scanner.NextInt(); // P

List<NetworkEdge> allEdges = new();
for (int i = 0; i < EdgeCount; i++)
{
    allEdges.Add(new NetworkEdge(new Railway
    {
        StationU = scanner.NextInt(),
        StationV = scanner.NextInt(),
        Capacity = scanner.NextInt()
    }));
}

List<int> routesToRemoveIndices = new();
for (int i = 0; i < RouteCount; i++)
{
    routesToRemoveIndices.Add(scanner.NextInt());
}


///
/// SOLVE
///

// Mark edges to be removed
bool[] isToBeRemoved = new bool[EdgeCount];
foreach (int idx in routesToRemoveIndices)
{
    isToBeRemoved[idx] = true;
}

// Initialize adjacency list
var adjacencyList = new List<NetworkEdge>[NodeCount];
for (int i = 0; i < NodeCount; i++) adjacencyList[i] = new();

// Add permanent edges
for (int i = 0; i < EdgeCount; i++)
{
    if (!isToBeRemoved[i])
    {
        adjacencyList[allEdges[i].U].Add(allEdges[i]);
        adjacencyList[allEdges[i].V].Add(allEdges[i]);
    }
}

// Work backwards through the removal list
int currentFlow = 0;

// Start with state where all P routes are removed
currentFlow = MaxFlow.CalculateMaxFlow(adjacencyList, currentFlow, 0, NodeCount - 1);

if (currentFlow >= C)
{
    Console.WriteLine($"{RouteCount} {currentFlow}");
    return;
}

// Add back routes in reverse order (from P-1 down to 0)
for (int i = RouteCount - 1; i >= 0; i--)
{
    int edgeIdx = routesToRemoveIndices[i];
    var edge = allEdges[edgeIdx];
    adjacencyList[edge.U].Add(edge);
    adjacencyList[edge.V].Add(edge);

    currentFlow = MaxFlow.CalculateMaxFlow(adjacencyList, currentFlow, 0, NodeCount - 1);

    if (currentFlow >= C)
    {
        Console.WriteLine($"{i} {currentFlow}");
        return;
    }
}
