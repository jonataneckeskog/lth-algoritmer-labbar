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

double smallestDistance = people
    .SelectMany((p1, index) => people.Skip(index + 1).Select(p2 => p1.Distance(p2)))
    .Min();


///
/// OUTPUT
/// 

Console.WriteLine(smallestDistance.ToString("F6", System.Globalization.CultureInfo.InvariantCulture));
