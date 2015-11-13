using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    class Floor : Tile
    {
        public Floor(int brightness, char symbol, ConsoleColor color) :
            base(brightness, symbol, color)
        {
        }
    }
}