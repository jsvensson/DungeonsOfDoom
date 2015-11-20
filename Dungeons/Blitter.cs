using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    static class Blitter
    {
        static List<Blixel> blixels = new List<Blixel>();

        public static void Draw()
        {
            if (blixels.Count >= 1)
            {
                foreach (Blixel blixel in blixels)
                {
                    DrawCharAtPos(blixel.Position, blixel.Symbol, blixel.Color);
                }
            }
            blixels.Clear();
        }

        public static void Add(Blixel blixel)
        {
            blixels.Add(blixel);
        }

        public static void Add(List<Blixel> blixels)
        {
            foreach (Blixel blixel in blixels)
            {
                Add(blixel);
            }
        }

        public static void Add(Point position, GameEntity entity)
        {
            Add(new Blixel(position, entity));
        }

        static void DrawCharAtPos(int x, int y, char character, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);
            Console.Write(character);
        }

        static void DrawCharAtPos(Point position, char character, ConsoleColor color)
        {
            DrawCharAtPos(position.X, position.Y, character, color);
        }

        static void DrawCharAtPos(Point position, GameEntity entity)
        {
            DrawCharAtPos(position.X, position.Y, entity.Symbol, entity.Color);
        }

    }
}
