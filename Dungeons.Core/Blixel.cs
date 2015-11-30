using System;

namespace Dungeons.Core
{
    class Blixel
    {
        internal Blixel(Point position, char symbol, ConsoleColor color)
        {
            Position = position;
            Symbol = symbol;
            Color = color;
        }

        internal Blixel(Point position, GameEntity entity)
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
