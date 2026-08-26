namespace RailDispatch.Domain.Map;

public sealed class GameMap
{
    private readonly MapCell[] _cells;

    public MapSize Size { get; }

    public GameMap(int width, int height)
        : this(new MapSize(width, height))
    {
    }

    public GameMap(MapSize size)
    {
        Size = size;
        _cells = new MapCell[size.Width * size.Height];

        for (var i = 0; i < _cells.Length; i++)
            _cells[i] = new MapCell();
    }

    public MapCell GetCell(MapPosition position)
    {
        ValidatePosition(position);
        return _cells[position.Y * Size.Width + position.X];
    }

    public void SetTerrain(MapPosition position, TerrainType terrain)
    {
        GetCell(position).Terrain = terrain;
    }

    private void ValidatePosition(MapPosition position)
    {
        if (position.X < 0 || position.X >= Size.Width)
            throw new ArgumentOutOfRangeException(nameof(position));

        if (position.Y < 0 || position.Y >= Size.Height)
            throw new ArgumentOutOfRangeException(nameof(position));
    }
}
