using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    class Item : GameEntity
    {
        public Item(string name, int weight, char symbol) : base(symbol)
        {
            Name = name;
            Weight = weight;
        }

        public string Name { get; private set; }
        public int Weight { get; private set; }
        public char Symbol { get; private set; }
    }
}
