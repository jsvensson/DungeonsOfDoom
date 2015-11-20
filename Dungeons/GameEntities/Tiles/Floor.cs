using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    class Floor : Tile
    {
        public Floor(char symbol, ConsoleColor color) :
            base(symbol, color)
        {
        }
        public override bool IsWalkable
        {
            get
            {
                return !HasMonster;
            }
        }
    }
}