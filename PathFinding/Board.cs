using Raylib_cs;
using System.Numerics;

namespace PathFindingAI
{
    internal class Board : IDrawable
    {
        private int _width;
        private int _height;
        private int _cellSizeLength;
        private int _margin;
        public CellType[,] Cells { get; private set; }
        public List<Vector2> Path { get; set; } = new List<Vector2>();
        public Board(AppSettings settings)
        {
            _width = settings.Width;
            _height = settings.Height;
            _cellSizeLength = settings.CellSizeLength;
            _margin = settings.Margin;

            Drawer.Drawables.Add(this);

            Cells = new CellType[((_width - _margin * 2) / _cellSizeLength), ((_height - _margin * 2) / _cellSizeLength)];
            Cells[0, 0] = CellType.StartPoint;
            Cells[5, 3] = CellType.Target;
        }
        public void Draw()
        {
            for (int x = _margin; x < _width - _margin; x += _cellSizeLength)
            {
                for (int y = _margin; y < _height - _margin; y += _cellSizeLength)
                {
                    Raylib.DrawRectangleLines(x, y, _cellSizeLength, _cellSizeLength, Color.BLACK);
                }
            }
            for (int x = 0; x < Cells.GetLength(0); x++)
            {
                for (int y = 0; y < Cells.GetLength(1); y++)
                {
                    switch (Cells[x, y])
                    {
                        case CellType.Wall:
                            {
                                Raylib.DrawRectangle(x * _cellSizeLength + _margin, y * _cellSizeLength + _margin,
                                    _cellSizeLength, _cellSizeLength, Color.BLACK);
                                break;
                            }
                        case CellType.Target:
                            {
                                Raylib.DrawRectangle(x * _cellSizeLength + _margin, y * _cellSizeLength + _margin,
                                 _cellSizeLength, _cellSizeLength, Color.RED);
                                Raylib.DrawRectangleLines(x * _cellSizeLength + _margin, y * _cellSizeLength + _margin,
                                    _cellSizeLength, _cellSizeLength, Color.BLACK);

                                break;
                            }
                        case CellType.StartPoint:
                            {
                                Raylib.DrawRectangle(x * _cellSizeLength + _margin, y * _cellSizeLength + _margin,
                                 _cellSizeLength, _cellSizeLength, Color.GREEN);
                                Raylib.DrawRectangleLines(x * _cellSizeLength + _margin, y * _cellSizeLength + _margin,
                                    _cellSizeLength, _cellSizeLength, Color.BLACK);

                                break;
                            }
                        default: break;
                    }
                }
            }
            foreach (Vector2 pathBlock in Path)
            {
                Raylib.DrawRectangle((int)pathBlock.X * _cellSizeLength + _margin, (int)pathBlock.Y * _cellSizeLength + _margin,
                    _cellSizeLength, _cellSizeLength, Color.PURPLE);
                Raylib.DrawRectangleLines((int)pathBlock.X * _cellSizeLength + _margin, (int)pathBlock.Y * _cellSizeLength + _margin,
                    _cellSizeLength, _cellSizeLength, Color.BLACK);
            }
        }
        public void SetCell(int mouseXPos, int mouseYPos, CellType cellType)
        {
            int cellX = (mouseXPos - _margin) / _cellSizeLength;
            int cellY = (mouseYPos - _margin) / _cellSizeLength;
            if (cellX < 0 || cellX >= Cells.GetLength(0) || cellY < 0 || cellY >= Cells.GetLength(1)) { return; }

            if (cellType == CellType.StartPoint || cellType == CellType.Target)
            {
                for (int x = 0; x < Cells.GetLength(0); x++)
                {
                    for (int y = 0; y < Cells.GetLength(1); y++)
                    {
                        if (Cells[x, y] == cellType)
                        {
                            Cells[x, y] = CellType.Empty;
                        }
                    }
                }

            }
            Cells[cellX, cellY] = cellType;

        }

    }
    public enum CellType
    {
        Empty,
        Wall,
        Target,
        StartPoint
    }
}
