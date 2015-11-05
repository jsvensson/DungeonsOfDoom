using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    class Tile
    {
        public Tile(int brightness)
        {
            Brightness = brightness;
        }

        public int Brightness { get; set; }
    }
}
