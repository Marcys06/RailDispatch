using RailDispatch.Domain.Map;

namespace RailDispatch.UI.Map;

public sealed class MapRenderer
{
    private readonly GameMap _map;

    public MapRenderer(GameMap map)
    {
        _map = map;
    }

    public void Render(Graphics graphics, Rectangle viewport, float zoom)
    {
        graphics.Clear(Color.White);

        var cellSize = Math.Max(1f, zoom);

        var startX = Math.Max(0, (int)(-viewport.X / cellSize));
        var startY = Math.Max(0, (int)(-viewport.Y / cellSize));

        var endX = Math.Min(
            _map.Size.Width,
            startX + (int)(viewport.Width / cellSize) + 2);

        var endY = Math.Min(
            _map.Size.Height,
            startY + (int)(viewport.Height / cellSize) + 2);

        using var grassBrush = new SolidBrush(Color.FromArgb(220, 220, 220));
        using var forestBrush = new SolidBrush(Color.FromArgb(170, 200, 170));
        using var hillBrush = new SolidBrush(Color.FromArgb(200, 190, 150));
        using var mountainBrush = new SolidBrush(Color.FromArgb(150, 150, 150));

        for (var y = startY; y < endY; y++)
        {
            for (var x = startX; x < endX; x++)
            {
                var terrain = _map.GetTerrain(new MapPosition(x, y));

                Brush brush = terrain switch
                {
                    TerrainType.Forest => forestBrush,
                    TerrainType.Hill => hillBrush,
                    TerrainType.Mountain => mountainBrush,
                    _ => grassBrush
                };

                graphics.FillRectangle(
                    brush,
                    x * cellSize,
                    y * cellSize,
                    cellSize + 1,
                    cellSize + 1);
            }
        }
    }
}
