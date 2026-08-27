using RailDispatch.Domain.Map;
using RailDispatch.Domain.Railway;

namespace RailDispatch.UI.Building;

public sealed class TrackBuilder
{
    private readonly GameMap _map;

    public TrackBuildMode Mode { get; set; } =
        TrackBuildMode.Straight;

    public CurveDirection Curve { get; set; } =
        CurveDirection.NorthEast;

    public bool StraightHorizontal { get; set; } = true;

    public TrackBuilder(GameMap map)
    {
        _map = map;
    }

    public void BuildStraight(
        MapPosition position,
        bool horizontal)
    {
        var connections =
            horizontal
                ? TrackConnections.West |
                  TrackConnections.East
                : TrackConnections.North |
                  TrackConnections.South;

        var track = GetOrCreate(position);

        track.SetGeometry(
            TrackGeometry.Straight);

        track.SetConnections(
            connections);

        ConnectNeighbours(position, connections);
    }

    public void BuildCurve(
        MapPosition position,
        CurveDirection direction)
    {
        var connections =
            direction switch
            {
                CurveDirection.NorthEast =>
                    TrackConnections.North |
                    TrackConnections.East,

                CurveDirection.EastSouth =>
                    TrackConnections.East |
                    TrackConnections.South,

                CurveDirection.SouthWest =>
                    TrackConnections.South |
                    TrackConnections.West,

                CurveDirection.WestNorth =>
                    TrackConnections.West |
                    TrackConnections.North,

                _ =>
                    TrackConnections.None
            };

        var track = GetOrCreate(position);

        track.SetGeometry(
            TrackGeometry.Curve);

        track.SetConnections(
            connections);

        ConnectNeighbours(position, connections);
    }

    public void Remove(
        MapPosition position)
    {
        _map.RemoveTrack(position);
    }

    private void ConnectNeighbours(
    MapPosition position,
    TrackConnections connections)
    {
        if (connections.HasFlag(TrackConnections.North))
        {
            ConnectNeighbour(
                position.X,
                position.Y - 1,
                TrackConnections.South);
        }

        if (connections.HasFlag(TrackConnections.East))
        {
            ConnectNeighbour(
                position.X + 1,
                position.Y,
                TrackConnections.West);
        }

        if (connections.HasFlag(TrackConnections.South))
        {
            ConnectNeighbour(
                position.X,
                position.Y + 1,
                TrackConnections.North);
        }

        if (connections.HasFlag(TrackConnections.West))
        {
            ConnectNeighbour(
                position.X - 1,
                position.Y,
                TrackConnections.East);
        }
    }

    private void ConnectNeighbour(
        int x,
        int y,
        TrackConnections connectionToAdd)
    {
        var neighbourPosition =
            new MapPosition(x, y);

        if (!_map.TryGetTrack(
                neighbourPosition,
                out var neighbour) ||
            neighbour is null)
        {
            return;
        }

        neighbour.SetConnections(
            neighbour.Connections |
            connectionToAdd);
    }

    private TrackCell GetOrCreate(
        MapPosition position)
    {
        if (_map.TryGetTrack(
                position,
                out var existing) &&
            existing is not null)
        {
            return existing;
        }

        var track = new TrackCell(
            position,
            TrackGeometry.Straight,
            TrackConnections.None);

        _map.AddTrack(track);

        return track;
    }
}
