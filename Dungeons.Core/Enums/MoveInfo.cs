using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons.Core
{
    enum MoveInfo
    {
        Success,
        Failed,
        Occupied,
        BlockedByWall,
        OutOfBounds,
    }
}
