using RailDispatch.Domain.Map;

namespace RailDispatch.Domain.Railway;

public sealed class TrackCell
{
    public MapPosition Position { get; }

    public TrackType Type { get; }

    public TrackConnections Connections { get; private set; }

    public TrackCell(
        MapPosition position,
        TrackType type,
        TrackConnections connections)
    {
        Position = position;
        Type = type;
        Connections = connections;
    }

    public void SetConnections(TrackConnections connections)
    {
        Connections = connections;
    }
}
