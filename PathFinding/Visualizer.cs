using Raylib_cs;
using System.Numerics;

namespace PathFindingAI
{
    internal class Visualizer : IDrawable
    {
        public bool Enabled { get; set; } = true;
        public List<Vector2> ClosedNodesPositons { get; set; } = new List<Vector2>();
        public List<Vector2> OpenNodesPositons { get; set; } = new List<Vector2>();
        public List<Vector2> Path { get; set; } = new List<Vector2>();

        private int _width;
        private int _height;
        private int _cellSizeLength;
        private int _margin;

        public Visualizer(AppSettings settings)
        {
            _width = settings.Width;
            _height = settings.Height;
            _cellSizeLength = settings.CellSizeLength;
            _margin = settings.Margin;

            Drawer.Drawables.Add(this);
        }

        public void Draw()
        {
            if (Enabled)
            {
                foreach (Vector2 position in ClosedNodesPositons)
                {
                    Raylib.DrawRectangle((int)position.X * _cellSizeLength + _margin, (int)position.Y * _cellSizeLength + _margin,
                    _cellSizeLength, _cellSizeLength, Color.GRAY);
                    Raylib.DrawRectangleLines((int)position.X * _cellSizeLength + _margin, (int)position.Y * _cellSizeLength + _margin,
                    _cellSizeLength, _cellSizeLength, Color.BLACK);

                }
                foreach (Vector2 position in OpenNodesPositons)
                {
                    Raylib.DrawRectangle((int)position.X * _cellSizeLength + _margin, (int)position.Y * _cellSizeLength + _margin,
                    _cellSizeLength, _cellSizeLength, Color.DARKGREEN);
                    Raylib.DrawRectangleLines((int)position.X * _cellSizeLength + _margin, (int)position.Y * _cellSizeLength + _margin,
                  _cellSizeLength, _cellSizeLength, Color.BLACK);
                }
                foreach (Vector2 position in Path)
                {
                    Raylib.DrawRectangle((int)position.X * _cellSizeLength + _margin, (int)position.Y * _cellSizeLength + _margin,
                    _cellSizeLength, _cellSizeLength, Color.PURPLE);
                    Raylib.DrawRectangleLines((int)position.X * _cellSizeLength + _margin, (int)position.Y * _cellSizeLength + _margin,
                  _cellSizeLength, _cellSizeLength, Color.BLACK);
                }
            }
        }
        public void Clear()
        {
            ClosedNodesPositons.Clear();
            OpenNodesPositons.Clear();
            Path.Clear();
        }
    }
}
