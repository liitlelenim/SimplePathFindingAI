using Raylib_cs;

namespace PathFindingAI
{
    internal class InputHandler
    {
        private CellType chosenCellType = CellType.Empty;
        private readonly Board board;
        private readonly PathFinder pathFinder;
        public InputHandler(Board board)
        {
            this.board = board;
            pathFinder = new PathFinder(board);
        }

        public void CheckForInput()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ZERO))
            {
                chosenCellType = CellType.Empty;
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ONE))
            {
                chosenCellType = CellType.Wall;
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_TWO))
            {
                chosenCellType = CellType.StartPoint;
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_THREE))
            {
                chosenCellType = CellType.Target;
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {

                board.Path = pathFinder.AStarSearch();
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_C))
            {
                board.Path = null;
            }
            if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
            {
                board.SetCell(Raylib.GetMouseX(), Raylib.GetMouseY(), chosenCellType);
            }
        }
    }
}
