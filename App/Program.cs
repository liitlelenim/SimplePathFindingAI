using PathFindingAI;
using Raylib_cs;

var appSettings = new AppSettings(width: 800, height: 800, cellSizeLength: 20, margin: 40);

Raylib.InitWindow(appSettings.Width, appSettings.Height, "Path-Finding");

var board = new Board(appSettings);
var visualizer = new Visualizer(appSettings);
var inputHandler = new InputHandler(board, visualizer);

while (!Raylib.WindowShouldClose())
{
    inputHandler.CheckForInput();
    Drawer.DrawCall();
}