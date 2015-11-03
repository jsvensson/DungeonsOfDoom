using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    class Player
    {
        public Player(string name, int health)
        {
            Name = name;
            Health = health;
        }

        public string Name { get; set; }
        public int Health { get; set; }
        public int AttackValue { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
