using System;

namespace Dungeons.Core
{
    class Floor : Tile
    {
        internal Floor(char symbol, ConsoleColor color) :
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