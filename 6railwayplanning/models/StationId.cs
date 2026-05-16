namespace _6railwayplanning;

public readonly record struct StationId(int Id)
{
    public static implicit operator int(StationId stationId) => stationId.Id;

    public static implicit operator StationId(int id) => new(id);
}