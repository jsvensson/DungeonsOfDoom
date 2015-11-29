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

        public virtual char Symbol { get; protected set; }
        public virtual ConsoleColor Color { get; protected set; }
    }
}
