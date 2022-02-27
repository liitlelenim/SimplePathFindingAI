using PathFindingAI;
using Raylib_cs;

int windowWidth = 800;
int windowHeight = 800;
Raylib.InitWindow(windowWidth, windowHeight, "Path-Finding");
Raylib.SetTargetFPS(60);

var board = new Board(windowWidth, windowHeight, 40, 40);
var inputHandler = new InputHandler(board);

while (!Raylib.WindowShouldClose())
{
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.WHITE);
    inputHandler.CheckForInput();
    board.Draw();
    Raylib.EndDrawing();
}