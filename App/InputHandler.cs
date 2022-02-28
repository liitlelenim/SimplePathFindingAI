using Raylib_cs;
using System.Numerics;

namespace PathFindingAI
{
    internal class InputHandler
    {
        private CellType chosenCellType = CellType.Empty;
        private readonly Board _board;
        private readonly Visualizer _visualizer;
        private readonly PathFinder _pathFinder;
        public InputHandler(Board board, Visualizer visualizer)
        {
            _board = board;
            _visualizer = visualizer;
            _pathFinder = new PathFinder(_board, _visualizer);
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
                _board.Path.Clear();
                _visualizer.Path.Clear();
                List<Vector2>? path = _pathFinder.AStarSearch();
                if (path != null)
                {
                    _board.Path = path;
                    _visualizer.Path = path;
                }
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_C))
            {
                _board.Path.Clear();
                _visualizer.Clear();

            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_V))
            {
                _visualizer.Enabled = !_visualizer.Enabled;
            }
            if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
            {
                _board.SetCell(Raylib.GetMouseX(), Raylib.GetMouseY(), chosenCellType);
                _visualizer.Clear();
            }
        }
    }
}
