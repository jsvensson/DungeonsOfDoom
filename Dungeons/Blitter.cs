using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    static class Blitter
    {
        static List<Blixel> blixels = new List<Blixel>();

        public static void Draw()
        {

        }

        public static void Add(Blixel blixel)
        {
            blixels.Add(blixel);
        }

        static void Clear()
        {
            blixels.Clear();
        }

    }
}
