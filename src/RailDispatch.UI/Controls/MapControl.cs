using RailDispatch.Domain.Map;
using RailDispatch.Domain.Railway;
using RailDispatch.Building;
using RailDispatch.UI.Map;

namespace RailDispatch.UI.Controls;

public sealed class MapControl : Control
{
    private readonly GameMap _map;
    private readonly MapRenderer _renderer;
    private readonly TrackBuilder _trackBuilder;

    private float _zoom = 4f;

    private PointF _camera;

    private Point _lastMousePosition;

    private bool _isDragging;

    public MapControl(GameMap map)
    {
        _map = map;
        _renderer = new MapRenderer(map);
        _trackBuilder = new TrackBuilder(map);

        DoubleBuffered = true;
        BackColor = Color.White;

        KeyDown += OnKeyDown;

        MouseWheel += OnMouseWheel;
        MouseDown += OnMouseDown;
        MouseMove += OnMouseMove;
        MouseUp += OnMouseUp;

        TabStop = false;
    }

    private void OnKeyDown(
        object? sender,
        KeyEventArgs e)
    {
        if (e.KeyCode == Keys.D1 ||
            e.KeyCode == Keys.NumPad1)
        {
            _trackBuilder.Mode =
                TrackBuildMode.Straight;

            Invalidate();
            return;
        }

        if (e.KeyCode == Keys.D2 ||
            e.KeyCode == Keys.NumPad2)
        {
            _trackBuilder.Mode =
                TrackBuildMode.Curve;

            Invalidate();
            return;
        }

        if (e.KeyCode == Keys.H &&
            _trackBuilder.Mode == TrackBuildMode.Straight)
        {
            _trackBuilder.StraightHorizontal = true;

            Invalidate();
            return;
        }

        if (e.KeyCode == Keys.V &&
            _trackBuilder.Mode == TrackBuildMode.Straight)
        {
            _trackBuilder.StraightHorizontal = false;

            Invalidate();
            return;
        }

        if (e.KeyCode == Keys.R &&
            _trackBuilder.Mode == TrackBuildMode.Curve)
        {
            _trackBuilder.Curve =
                _trackBuilder.Curve switch
                {
                    CurveDirection.NorthEast =>
                        CurveDirection.EastSouth,

                    CurveDirection.EastSouth =>
                        CurveDirection.SouthWest,

                    CurveDirection.SouthWest =>
                        CurveDirection.WestNorth,

                    CurveDirection.WestNorth =>
                        CurveDirection.NorthEast,

                    _ =>
                        CurveDirection.NorthEast
                };

            Invalidate();
        }
    }

    protected override void OnPaint(
        PaintEventArgs e)
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

        e.Graphics.ResetTransform();

        DrawToolPanel(e.Graphics);
    }

    private void DrawToolPanel(
        Graphics graphics)
    {
        const int x = 15;
        const int y = 15;
        const int width = 250;
        const int height = 145;

        using var background =
            new SolidBrush(
                Color.FromArgb(
                    225,
                    30,
                    30,
                    30));

        using var border =
            new Pen(Color.White, 1);

        using var textBrush =
            new SolidBrush(Color.White);

        using var titleFont =
            new Font(
                "Segoe UI",
                10,
                FontStyle.Bold);

        using var font =
            new Font(
                "Segoe UI",
                9,
                FontStyle.Regular);

        graphics.FillRectangle(
            background,
            x,
            y,
            width,
            height);

        graphics.DrawRectangle(
            border,
            x,
            y,
            width,
            height);

        var toolName =
            _trackBuilder.Mode ==
            TrackBuildMode.Straight
                ? "TOR PROSTY"
                : "ZAKRĘT";

        var orientation =
            _trackBuilder.Mode ==
            TrackBuildMode.Straight
                ? "Orientacja: " +
                  (_trackBuilder.StraightHorizontal
                      ? "POZIOMA"
                      : "PIONOWA")
                : "Kierunek: " +
                  GetCurveName(
                      _trackBuilder.Curve);

        graphics.DrawString(
            "NARZĘDZIE: " + toolName,
            titleFont,
            textBrush,
            x + 10,
            y + 10);

        graphics.DrawString(
            orientation,
            font,
            textBrush,
            x + 10,
            y + 38);

        graphics.DrawString(
            "[1] Tor prosty",
            font,
            textBrush,
            x + 10,
            y + 62);

        graphics.DrawString(
            "[2] Zakręt",
            font,
            textBrush,
            x + 120,
            y + 62);

        graphics.DrawString(
            "[H] Poziomy",
            font,
            textBrush,
            x + 10,
            y + 84);

        graphics.DrawString(
            "[V] Pionowy",
            font,
            textBrush,
            x + 120,
            y + 84);

        graphics.DrawString(
            "[R] Obróć zakręt",
            font,
            textBrush,
            x + 10,
            y + 106);

        graphics.DrawString(
            "LPM = postaw",
            font,
            textBrush,
            x + 10,
            y + 126);
    }

    private static string GetCurveName(
        CurveDirection direction)
    {
        return direction switch
        {
            CurveDirection.NorthEast =>
                "N → E",

            CurveDirection.EastSouth =>
                "E → S",

            CurveDirection.SouthWest =>
                "S → W",

            CurveDirection.WestNorth =>
                "W → N",

            _ =>
                "?"
        };
    }

    private void RenderTracks(
        Graphics graphics)
    {
        var cellSize = _zoom;

        using var pen =
            new Pen(
                Color.Black,
                Math.Max(
                    1f,
                    cellSize * 0.22f));

        foreach (var track in _map.Tracks.Values)
        {
            if (track.Geometry ==
                TrackGeometry.Curve)
            {
                RenderCurve(
                    graphics,
                    pen,
                    track,
                    cellSize);

                continue;
            }

            RenderStraight(
                graphics,
                pen,
                track,
                cellSize);
        }
    }

    private static void RenderStraight(
        Graphics graphics,
        Pen pen,
        TrackCell track,
        float cellSize)
    {
        var x =
            track.Position.X *
            cellSize;

        var y =
            track.Position.Y *
            cellSize;

        var centerX =
            x + cellSize / 2f;

        var centerY =
            y + cellSize / 2f;

        if (track.HasConnection(
                TrackConnections.North))
        {
            graphics.DrawLine(
                pen,
                centerX,
                centerY,
                centerX,
                y);
        }

        if (track.HasConnection(
                TrackConnections.East))
        {
            graphics.DrawLine(
                pen,
                centerX,
                centerY,
                x + cellSize,
                centerY);
        }

        if (track.HasConnection(
                TrackConnections.South))
        {
            graphics.DrawLine(
                pen,
                centerX,
                centerY,
                centerX,
                y + cellSize);
        }

        if (track.HasConnection(
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

   
private static void RenderCurve(
    Graphics graphics,
    Pen pen,
    TrackCell track,
    float cellSize)
    {
        var x =
            track.Position.X * cellSize;

        var y =
            track.Position.Y * cellSize;

        var centerX =
            x + cellSize / 2f;

        var centerY =
            y + cellSize / 2f;

        /*
         * Punkty końcowe znajdują się w środkach
         * sąsiednich pól mapy.
         *
         *        N
         *        |
         *        |
         *        ╰──── E
         *
         * Dzięki temu tor sąsiadujący łączy się
         * dokładnie z końcem zakrętu.
         */

        var north =
            new PointF(
                centerX,
                centerY - cellSize);

        var east =
            new PointF(
                centerX + cellSize,
                centerY);

        var south =
            new PointF(
                centerX,
                centerY + cellSize);

        var west =
            new PointF(
                centerX - cellSize,
                centerY);

        /*
         * Odległość punktów kontrolnych.
         *
         * Wartość 0.5522848 odpowiada przybliżeniu
         * ćwiartki okręgu krzywą Béziera.
         */

        const float k = 0.5522848f;

        var offset =
            cellSize * k;

        /*
         * N → E
         */

        if (track.HasConnection(
                TrackConnections.North) &&
            track.HasConnection(
                TrackConnections.East))
        {
            graphics.DrawBezier(
                pen,
                north,
                new PointF(
                    north.X + offset,
                    north.Y),

                new PointF(
                    east.X,
                    east.Y - offset),

                east);

            return;
        }

        /*
         * E → S
         */

        if (track.HasConnection(
                TrackConnections.East) &&
            track.HasConnection(
                TrackConnections.South))
        {
            graphics.DrawBezier(
                pen,
                east,
                new PointF(
                    east.X,
                    east.Y + offset),

                new PointF(
                    south.X + offset,
                    south.Y),

                south);

            return;
        }

        /*
         * S → W
         */

        if (track.HasConnection(
                TrackConnections.South) &&
            track.HasConnection(
                TrackConnections.West))
        {
            graphics.DrawBezier(
                pen,
                south,
                new PointF(
                    south.X - offset,
                    south.Y),

                new PointF(
                    west.X,
                    west.Y + offset),

                west);

            return;
        }

        /*
         * W → N
         */

        if (track.HasConnection(
                TrackConnections.West) &&
            track.HasConnection(
                TrackConnections.North))
        {
            graphics.DrawBezier(
                pen,
                west,
                new PointF(
                    west.X,
                    west.Y - offset),

                new PointF(
                    north.X - offset,
                    north.Y),

                north);
        }
    }



    private void OnMouseDown(
        object? sender,
        MouseEventArgs e)
    {
        Focus();

        if (e.Button == MouseButtons.Middle)
        {
            _isDragging = true;
            _lastMousePosition = e.Location;
            return;
        }

        var position =
            ScreenToMap(e.Location);

        if (e.Button == MouseButtons.Right)
        {
            _trackBuilder.Remove(position);

            Invalidate();
            return;
        }

        if (e.Button != MouseButtons.Left)
            return;

        if (_trackBuilder.Mode ==
            TrackBuildMode.Straight)
        {
            _trackBuilder.BuildStraight(
                position,
                _trackBuilder.StraightHorizontal);

            Invalidate();
            return;
        }

        if (_trackBuilder.Mode ==
            TrackBuildMode.Curve)
        {
            _trackBuilder.BuildCurve(
                position,
                _trackBuilder.Curve);

            Invalidate();
        }
    }

    private void OnMouseMove(
        object? sender,
        MouseEventArgs e)
    {
        if (!_isDragging)
            return;

        var deltaX =
            e.X -
            _lastMousePosition.X;

        var deltaY =
            e.Y -
            _lastMousePosition.Y;

        _camera.X -= deltaX;
        _camera.Y -= deltaY;

        _lastMousePosition =
            e.Location;

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
    }

    private void OnMouseWheel(
        object? sender,
        MouseEventArgs e)
    {
        var oldZoom = _zoom;

        if (e.Delta > 0)
        {
            _zoom *= 1.15f;
        }
        else if (e.Delta < 0)
        {
            _zoom /= 1.15f;
        }

        _zoom =
            Math.Clamp(
                _zoom,
                2f,
                40f);

        if (Math.Abs(
                _zoom -
                oldZoom) < 0.001f)
        {
            return;
        }

        var mouseMapX =
            (_camera.X + e.X) /
            oldZoom;

        var mouseMapY =
            (_camera.Y + e.Y) /
            oldZoom;

        _camera.X =
            mouseMapX * _zoom -
            e.X;

        _camera.Y =
            mouseMapY * _zoom -
            e.Y;

        ClampCamera();

        Invalidate();
    }

    private MapPosition ScreenToMap(
        Point point)
    {
        var mapX =
            (_camera.X + point.X) /
            _zoom;

        var mapY =
            (_camera.Y + point.Y) /
            _zoom;

        return new MapPosition(
            (int)Math.Floor(mapX),
            (int)Math.Floor(mapY));
    }

    private void ClampCamera()
    {
        var mapWidth =
            _map.Size.Width *
            _zoom;

        var mapHeight =
            _map.Size.Height *
            _zoom;

        _camera.X =
            Math.Clamp(
                _camera.X,
                0,
                Math.Max(
                    0,
                    mapWidth - Width));

        _camera.Y =
            Math.Clamp(
                _camera.Y,
                0,
                Math.Max(
                    0,
                    mapHeight - Height));
    }
}
