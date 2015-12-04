using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons.Core
{
    interface ILootable
    {
        int Weight { get; set; }
        string Name { get; set; }
    }
}
