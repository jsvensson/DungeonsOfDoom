using System;

namespace Dungeons.Core
{
    abstract class GameEntity
    {
        protected GameEntity(char symbol, ConsoleColor color)
        {
            Symbol = symbol;
            Color = color;
        }

        public virtual char Symbol { get; protected set; }
        public virtual ConsoleColor Color { get; protected set; }
    }
}
