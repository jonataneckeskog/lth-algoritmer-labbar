using _4closestpair;

///
/// LOAD DATA
/// 

var scanner = new Scanner(Console.OpenStandardInput());

int PersonCount = scanner.NextInt();

Vector2Int[] people = scanner.AsEnumerable()
    .Take(PersonCount * 2)
    .Chunk(2)
    .Select(p => new Vector2Int(p[0], p[1]))
    .ToArray();


///
/// SOLVE
///

Array.Sort(people, (p1, p2) => p1.X.CompareTo(p2.X));

double smallestDistance = Solve(people);

double Solve(Span<Vector2Int> pts)
{
    if (pts.Length <= 3) return BruteForce(pts.ToArray());

    int mid = pts.Length / 2;
    int midX = pts[mid].X;

    double dL = Solve(pts[..mid]);
    double dR = Solve(pts[mid..]);
    double d = Math.Min(dL, dR);

    var strip = new List<Vector2Int>();
    for (int i = 0; i < pts.Length; i++)
    {
        if (Math.Abs(pts[i].X - midX) < d) strip.Add(pts[i]);
    }

    // Sort the strip by Y
    strip.Sort((a, b) => a.Y.CompareTo(b.Y));

    // Check neighbors in the strip
    for (int i = 0; i < strip.Count; i++)
    {
        for (int j = i + 1; j < strip.Count && (strip[j].Y - strip[i].Y) < d; j++)
        {
            d = Math.Min(d, strip[i].Distance(strip[j]));
        }
    }

    return d;
}

double BruteForce(Vector2Int[] pts) => pts
    .SelectMany((p1, i) => pts[(i + 1)..].Select(p2 => p1.Distance(p2)))
    .DefaultIfEmpty(0)
    .Min();


///
/// OUTPUT
/// 

Console.WriteLine(smallestDistance.ToString("F6", System.Globalization.CultureInfo.InvariantCulture));
