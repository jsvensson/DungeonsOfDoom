using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    class Wall : Tile
    {
        public Wall(int brightness, char symbol, ConsoleColor color) :
            base(brightness, symbol, color)
        {
            IsWalkable = false;
        }
    }
}