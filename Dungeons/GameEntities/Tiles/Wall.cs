using System;

namespace Dungeons
{
    class Wall : Tile
    {
        public Wall(int brightness, char symbol, ConsoleColor color) :
            base(symbol, color)
        {
            IsWalkable = false;
        }
    }
}