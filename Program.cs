using System;

namespace MyMinecraftClone
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            // Creates game object and runs it
            using (var game = new Game1())
            {
                game.Run();
            }
        }
    }
}
