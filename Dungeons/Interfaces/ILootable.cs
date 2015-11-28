using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    interface ILootable
    {
        int Weight { get; set; }
        string Name { get; set; }
    }
}
