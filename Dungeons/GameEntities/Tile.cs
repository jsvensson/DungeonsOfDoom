using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    abstract class Tile : GameEntity
    {
        public Tile(int brightness, char symbol, ConsoleColor color) :
            base(symbol, color)
        {
            IsWalkable = true;
            Brightness = brightness;
            Item = null;
        }

        public bool HasItems
        {
            get
            {
                return Item != null;
            }
        }

        public bool HasMonster
        {
            get
            {
                return Monster != null;
            }
        }

        public int Brightness { get; set; }
        public Item Item { get; set; }
        public Monster Monster { get; set; }
        public bool IsWalkable { get; set; }
    }
}
