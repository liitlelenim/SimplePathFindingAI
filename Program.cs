using PathFindingAI;
using Raylib_cs;

var appSettings = new AppSettings(width: 800, height: 800, cellSizeLength: 40, margin: 40);

Raylib.InitWindow(appSettings.Width, appSettings.Height, "Path-Finding");
Raylib.SetTargetFPS(60);

var board = new Board(appSettings);
var inputHandler = new InputHandler(board);

while (!Raylib.WindowShouldClose())
{
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.WHITE);
    inputHandler.CheckForInput();
    board.Draw();
    Raylib.EndDrawing();
}