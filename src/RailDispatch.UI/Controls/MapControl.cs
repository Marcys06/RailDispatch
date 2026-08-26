using RailDispatch.Domain.Map;
using RailDispatch.Domain.Railway;
using RailDispatch.UI.Map;

namespace RailDispatch.UI.Controls;

public sealed class MapControl : Control
{
    private readonly GameMap _map;
    private readonly MapRenderer _renderer;

    private float _zoom = 4f;

    private PointF _camera;

    private Point _lastMousePosition;

    private bool _isDragging;

    public MapControl(GameMap map)
    {
        _map = map;
        _renderer = new MapRenderer(map);

        DoubleBuffered = true;
        BackColor = Color.White;

        MouseWheel += OnMouseWheel;
        MouseDown += OnMouseDown;
        MouseMove += OnMouseMove;
        MouseUp += OnMouseUp;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        e.Graphics.Clear(Color.White);

        e.Graphics.TranslateTransform(
            -_camera.X,
            -_camera.Y);

        _renderer.Render(
            e.Graphics,
            new Rectangle(
                (int)_camera.X,
                (int)_camera.Y,
                Width,
                Height),
            _zoom);

        RenderTracks(e.Graphics);
    }

    private void RenderTracks(Graphics graphics)
    {
        var cellSize = _zoom;

        using var pen = new Pen(
            Color.Black,
            Math.Max(1f, cellSize * 0.25f));

        foreach (var track in _map.Tracks.Values)
        {
            var x =
                track.Position.X * cellSize;

            var y =
                track.Position.Y * cellSize;

            var centerX =
                x + cellSize / 2f;

            var centerY =
                y + cellSize / 2f;

            if (track.Connections.HasFlag(
                    TrackConnections.North))
            {
                graphics.DrawLine(
                    pen,
                    centerX,
                    centerY,
                    centerX,
                    y);
            }

            if (track.Connections.HasFlag(
                    TrackConnections.East))
            {
                graphics.DrawLine(
                    pen,
                    centerX,
                    centerY,
                    x + cellSize,
                    centerY);
            }

            if (track.Connections.HasFlag(
                    TrackConnections.South))
            {
                graphics.DrawLine(
                    pen,
                    centerX,
                    centerY,
                    centerX,
                    y + cellSize);
            }

            if (track.Connections.HasFlag(
                    TrackConnections.West))
            {
                graphics.DrawLine(
                    pen,
                    centerX,
                    centerY,
                    x,
                    centerY);
            }
        }
    }

    private void OnMouseDown(
        object? sender,
        MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            var position = ScreenToMap(e.Location);

            BuildTrack(position);

            Invalidate();

            return;
        }

        if (e.Button == MouseButtons.Right)
        {
            var position = ScreenToMap(e.Location);

            _map.RemoveTrack(position);

            Invalidate();

            return;
        }

        if (e.Button == MouseButtons.Middle)
        {
            _isDragging = true;
            _lastMousePosition = e.Location;

            Cursor = Cursors.Hand;
        }
    }

    private void OnMouseMove(
        object? sender,
        MouseEventArgs e)
    {
        if (!_isDragging)
            return;

        var deltaX =
            e.X - _lastMousePosition.X;

        var deltaY =
            e.Y - _lastMousePosition.Y;

        _camera.X -= deltaX;
        _camera.Y -= deltaY;

        _lastMousePosition = e.Location;

        ClampCamera();

        Invalidate();
    }

    private void OnMouseUp(
        object? sender,
        MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Middle)
            return;

        _isDragging = false;

        Cursor = Cursors.Default;
    }

    private void OnMouseWheel(
        object? sender,
        MouseEventArgs e)
    {
        var oldZoom = _zoom;

        var mouseWorldX =
            (_camera.X + e.X) / oldZoom;

        var mouseWorldY =
            (_camera.Y + e.Y) / oldZoom;

        _zoom *=
            e.Delta > 0
                ? 1.2f
                : 0.8f;

        _zoom = Math.Clamp(
            _zoom,
            0.25f,
            64f);

        _camera.X =
            mouseWorldX * _zoom - e.X;

        _camera.Y =
            mouseWorldY * _zoom - e.Y;

        ClampCamera();

        Invalidate();
    }

    private MapPosition ScreenToMap(Point point)
    {
        var worldX =
            (_camera.X + point.X) / _zoom;

        var worldY =
            (_camera.Y + point.Y) / _zoom;

        return new MapPosition(
            (int)Math.Floor(worldX),
            (int)Math.Floor(worldY));
    }

    private void BuildTrack(
        MapPosition position)
    {
        if (position.X < 0 ||
            position.X >= _map.Size.Width ||
            position.Y < 0 ||
            position.Y >= _map.Size.Height)
        {
            return;
        }

        if (_map.HasTrack(position))
            return;

        var connections =
            TrackConnections.East |
            TrackConnections.West;

        var track = new TrackCell(
            position,
            TrackType.Straight,
            connections);

        _map.AddTrack(track);
    }

    private void ClampCamera()
    {
        var mapWidth =
            _map.Size.Width * _zoom;

        var mapHeight =
            _map.Size.Height * _zoom;

        var maxX =
            Math.Max(0, mapWidth - Width);

        var maxY =
            Math.Max(0, mapHeight - Height);

        _camera.X =
            Math.Clamp(
                _camera.X,
                0,
                maxX);

        _camera.Y =
            Math.Clamp(
                _camera.Y,
                0,
                maxY);
    }
}
