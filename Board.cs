using Raylib_cs;
using System.Numerics;

namespace PathFindingAI
{
    internal class Board
    {
        public int Width { get; init; }
        public int Height { get; init; }
        private int cellSizeLength;
        private int margin;
        public CellType[,] Cells { get; private set; }
        public List<Vector2> Path { get; set; } = null;
        public Board(int width, int height, int cellSizeLength, int margin)
        {
            Width = width;
            Height = height;
            this.cellSizeLength = cellSizeLength;
            this.margin = margin;
            Cells = new CellType[((width - margin * 2) / cellSizeLength), ((height - margin * 2) / cellSizeLength)];
            Cells[0, 0] = CellType.StartPoint;
            Cells[5, 3] = CellType.Target;


        }
        public void Draw()
        {
            for (int x = margin; x < Width - margin; x += cellSizeLength)
            {
                for (int y = margin; y < Height - margin; y += cellSizeLength)
                {
                    Raylib.DrawRectangleLines(x, y, cellSizeLength, cellSizeLength, Color.BLACK);
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
                                Raylib.DrawRectangle(x * cellSizeLength + margin, y * cellSizeLength + margin,
                                    cellSizeLength, cellSizeLength, Color.BLACK);
                                break;
                            }
                        case CellType.Target:
                            {
                                Raylib.DrawRectangle(x * cellSizeLength + margin, y * cellSizeLength + margin,
                                 cellSizeLength, cellSizeLength, Color.RED);
                                break;
                            }
                        case CellType.StartPoint:
                            {
                                Raylib.DrawRectangle(x * cellSizeLength + margin, y * cellSizeLength + margin,
                                 cellSizeLength, cellSizeLength, Color.GREEN);
                                break;
                            }
                        default: break;
                    }
                }
            }
            if (Path != null)
            {

                foreach (Vector2 pathBlock in Path)
                {
                    Raylib.DrawRectangle((int)pathBlock.X * cellSizeLength + margin, (int)pathBlock.Y * cellSizeLength + margin,
                        cellSizeLength, cellSizeLength, Color.PURPLE);
                }
            }
        }
        public void SetCell(int mouseXPos, int mouseYPos, CellType cellType)
        {
            int cellX = (mouseXPos - margin) / cellSizeLength;
            int cellY = (mouseYPos - margin) / cellSizeLength;
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
