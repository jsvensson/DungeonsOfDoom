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
            Item = null;
        }

        public int Brightness { get; set; }
        public Item Item { get; set; }
    }
}
