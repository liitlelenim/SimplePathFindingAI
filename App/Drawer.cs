using Raylib_cs;

namespace PathFindingAI
{
    internal static class Drawer
    {
        public static List<IDrawable> Drawables { get; set; } = new List<IDrawable>();

        public static void DrawCall()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);
            foreach (IDrawable drawable in Drawables)
            {
                drawable.Draw();
            }
            Raylib.EndDrawing();
        }
    }
}
