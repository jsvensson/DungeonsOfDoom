using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    class Item
    {
        public Item(string name, int weight, char symbol)
        {
            Name = name;
            Weight = weight;
            Symbol = symbol;
        }

        public string Name { get; private set; }
        public int Weight { get; private set; }
        public char Symbol { get; private set; }
    }
}
