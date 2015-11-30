using System;

namespace Dungeons.Core
{
    public class Blixel
    {
        public Blixel(Point position, char symbol, ConsoleColor color)
        {
            Position = position;
            Symbol = symbol;
            Color = color;
        }

        public Blixel(Point position, GameEntity entity)
        {
            Position = position;
            Symbol = entity.Symbol;
            Color = entity.Color;
        }

        public char Symbol { get; }
        public ConsoleColor Color { get; }
        public Point Position { get; }
    }
}
