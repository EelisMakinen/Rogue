using ZeroElectric.Vinculum;
namespace raylib_pohja
{
    class WindowTest
    {
        const int screen_width = 240;
        const int screen_height = 136;
        public WindowTest()
        {

        }

        public void Run()
        {
            Raylib.InitWindow(screen_width, screen_height, "Raylib");
            Raylib.SetTargetFPS(60);

            while (Raylib.WindowShouldClose() == false)
            {
                Update();
                Draw();
            }

            Raylib.CloseWindow();
        }

        private void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.YELLOW);
            // Draws a maroon circle in the middle
            Raylib.DrawCircle(screen_width / 2, screen_height / 2, 20, Raylib.MAROON);

            // Draw rest of the game here

            Raylib.EndDrawing();
        }

        private void Update()
        {
            // Update game here
        }
    }
}