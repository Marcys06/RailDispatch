using RailDispatch.Domain.Map;
using RailDispatch.UI.Controls;

namespace RailDispatch.App;

public sealed class MainForm : Form
{
    public MainForm()
    {
        Text = "RailDispatch";
        WindowState = FormWindowState.Maximized;

        var map = new GameMap(512, 512);

        var mapControl = new MapControl(map)
        {
            Dock = DockStyle.Fill
        };

        Controls.Add(mapControl);
    }
}
