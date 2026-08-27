using RailDispatch.Domain.Map;
using RailDispatch.Domain.Railway;
using RailDispatch.Building;

namespace RailDispatch.Tests;

public class TrackBuilderTests
{
    [Fact]
    public void BuildStraight_ShouldConnectToExistingNeighbour_Horizontally()
    {
        var map = new GameMap(10, 10);
        var builder = new TrackBuilder(map);

        builder.BuildStraight(new MapPosition(5, 5), horizontal: true);
        builder.BuildStraight(new MapPosition(6, 5), horizontal: true);

        map.TryGetTrack(new MapPosition(5, 5), out var left);
        map.TryGetTrack(new MapPosition(6, 5), out var right);

        Assert.True(left!.HasConnection(TrackConnections.East));
        Assert.True(right!.HasConnection(TrackConnections.West));
    }

    [Fact]
    public void BuildStraight_ShouldConnectToExistingNeighbour_Vertically()
    {
        var map = new GameMap(10, 10);
        var builder = new TrackBuilder(map);

        builder.BuildStraight(new MapPosition(5, 5), horizontal: false);
        builder.BuildStraight(new MapPosition(5, 6), horizontal: false);

        map.TryGetTrack(new MapPosition(5, 5), out var top);
        map.TryGetTrack(new MapPosition(5, 6), out var bottom);

        Assert.True(top!.HasConnection(TrackConnections.South));
        Assert.True(bottom!.HasConnection(TrackConnections.North));
    }

    [Fact]
    public void BuildCurve_ShouldConnectToExistingStraightNeighbour()
    {
        var map = new GameMap(10, 10);
        var builder = new TrackBuilder(map);

        builder.BuildStraight(new MapPosition(4, 5), horizontal: true);
        builder.BuildCurve(new MapPosition(5, 5), CurveDirection.WestNorth);

        map.TryGetTrack(new MapPosition(4, 5), out var straight);
        map.TryGetTrack(new MapPosition(5, 5), out var curve);

        Assert.True(straight!.HasConnection(TrackConnections.East));
        Assert.True(curve!.HasConnection(TrackConnections.West));
    }
}
