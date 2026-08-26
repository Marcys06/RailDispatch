using RailDispatch.Domain.Map;

namespace RailDispatch.Tests;

public class GameMapTests
{
    [Fact]
    public void NewMap_ShouldContainGrass()
    {
        var map = new GameMap(100, 100);

        var terrain = map.GetTerrain(new MapPosition(10, 20));

        Assert.Equal(TerrainType.Grass, terrain);
    }

    [Fact]
    public void SetTerrain_ShouldChangeTerrain()
    {
        var map = new GameMap(100, 100);
        var position = new MapPosition(10, 20);

        map.SetTerrain(position, TerrainType.Forest);

        Assert.Equal(TerrainType.Forest, map.GetTerrain(position));
    }

    [Fact]
    public void Map_ShouldRejectCoordinatesOutsideBounds()
    {
        var map = new GameMap(100, 100);

        Assert.Throws<ArgumentOutOfRangeException>(
            () => map.GetTerrain(new MapPosition(100, 50)));
    }

    [Fact]
    public void MapSize_ShouldRejectDimensionsAboveMaximum()
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new MapSize(16385, 100));
    }
}
