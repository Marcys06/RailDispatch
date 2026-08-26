using RailDispatch.Domain.Railway;

namespace RailDispatch.Domain.Map;

public sealed class GameMap
{
    private readonly TerrainType[] _terrain;
    private readonly Dictionary<MapPosition, TrackCell> _tracks = new();

    public MapSize Size { get; }

    public IReadOnlyDictionary<MapPosition, TrackCell> Tracks =>
        _tracks;

    public GameMap(int width, int height)
        : this(new MapSize(width, height))
    {
    }

    public GameMap(MapSize size)
    {
        Size = size;

        _terrain = new TerrainType[
            checked(size.Width * size.Height)];
    }

    public TerrainType GetTerrain(
        MapPosition position)
    {
        ValidatePosition(position);

        return _terrain[GetIndex(position)];
    }

    public void SetTerrain(
        MapPosition position,
        TerrainType terrain)
    {
        ValidatePosition(position);

        _terrain[GetIndex(position)] = terrain;
    }

    public bool HasTrack(
        MapPosition position)
    {
        return _tracks.ContainsKey(position);
    }

    public bool TryGetTrack(
        MapPosition position,
        out TrackCell? track)
    {
        return _tracks.TryGetValue(
            position,
            out track);
    }

    public void AddTrack(
        TrackCell track)
    {
        ValidatePosition(track.Position);

        _tracks[track.Position] = track;
    }

    public bool RemoveTrack(
        MapPosition position)
    {
        return _tracks.Remove(position);
    }

    private int GetIndex(
        MapPosition position)
    {
        return checked(
            position.Y * Size.Width +
            position.X);
    }

    private void ValidatePosition(
        MapPosition position)
    {
        if (position.X < 0 ||
            position.X >= Size.Width ||
            position.Y < 0 ||
            position.Y >= Size.Height)
        {
            throw new ArgumentOutOfRangeException(
                nameof(position));
        }
    }
}
