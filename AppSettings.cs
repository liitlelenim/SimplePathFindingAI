namespace PathFindingAI
{
    internal class AppSettings
    {
        public int Width { get; init; }
        public int Height { get; init; }
        public int CellSizeLength { get; init; }
        public int Margin { get; init; }

        public AppSettings(int width, int height, int cellSizeLength, int margin)
        {
            Width = width;
            Height = height;
            CellSizeLength = cellSizeLength;
            Margin = margin;
        }
        private AppSettings() { }
    }
}
