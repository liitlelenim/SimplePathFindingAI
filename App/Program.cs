using PathFindingAI;
using PathFindingAI.SettingsHandling;
using Raylib_cs;

var appSettings = SettingsLoader.Load();
Raylib.InitWindow(appSettings.Width, appSettings.Height, "Path-Finding");

var board = new Board(appSettings);
var visualizer = new Visualizer(appSettings);
var inputHandler = new InputHandler(board, visualizer);

while (!Raylib.WindowShouldClose())
{
    inputHandler.CheckForInput();
    Drawer.DrawCall();
}