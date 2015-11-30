using System;
using Dungeons.Core;

namespace Dungeons
{
    class Program
    {
        static void Main()
        {
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;
            Game game = new Game(width, height);
            game.Start();
        }
    }
}
