using System;

namespace Dungeons
{
    abstract class GameEntity
    {
        public GameEntity(char symbol, ConsoleColor color)
        {
            Symbol = symbol;
            Color = color;
        }

        public virtual char Symbol { get; set; }
        public ConsoleColor Color { get; set; }
    }
}
