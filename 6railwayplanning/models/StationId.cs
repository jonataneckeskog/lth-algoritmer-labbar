namespace _6railwayplanning;

public readonly struct StationId(int Id)
{
    public int Id { get; } = Id;

    public static implicit operator int(StationId stationId) => stationId.Id;

    public static implicit operator StationId(int id) => new(id);
}