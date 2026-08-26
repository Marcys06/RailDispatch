namespace RailDispatch.Domain.Map;

public sealed class GameMap
{
    private readonly TerrainType[] _terrain;

    public MapSize Size { get; }

    public GameMap(int width, int height)
        : this(new MapSize(width, height))
    {
    }

    public GameMap(MapSize size)
    {
        Size = size;
        _terrain = new TerrainType[checked(size.Width * size.Height)];
    }

    public TerrainType GetTerrain(MapPosition position)
    {
        ValidatePosition(position);
        return _terrain[GetIndex(position)];
    }

    public void SetTerrain(MapPosition position, TerrainType terrain)
    {
        ValidatePosition(position);
        _terrain[GetIndex(position)] = terrain;
    }

    private int GetIndex(MapPosition position)
    {
        return checked(position.Y * Size.Width + position.X);
    }

    private void ValidatePosition(MapPosition position)
    {
        if (position.X < 0 || position.X >= Size.Width)
            throw new ArgumentOutOfRangeException(nameof(position));

        if (position.Y < 0 || position.Y >= Size.Height)
            throw new ArgumentOutOfRangeException(nameof(position));
    }
}
