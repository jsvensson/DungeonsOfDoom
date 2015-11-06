using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    struct Position
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }

        public static bool Compare(Position left, Position right)
        {
            return (left.X == right.X) && (left.Y == right.Y);
        }
    }
}
