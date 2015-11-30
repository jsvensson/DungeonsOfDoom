using System;

namespace Dungeons.Core
{
    class Wall : Tile
    {
        internal Wall(int brightness, char symbol, ConsoleColor color) :
            base(symbol, color)
        {
            IsWalkable = false;
        }
    }
}