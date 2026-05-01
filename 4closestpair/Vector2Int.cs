namespace _4closestpair;

public readonly struct Vector2Int
{
    public readonly int X;
    public readonly int Y;

    public Vector2Int(int x, int y) => (X, Y) = (x, y);

    public double Distance(Vector2Int other)
    {
        long dx = X - other.X;
        long dy = Y - other.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}
