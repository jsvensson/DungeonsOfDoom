using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    class Creature
    {
        public Creature(string name, int health, int attackValue)
        {
            Name = name;
            Health = health;
            AttackValue = attackValue;
        }

        public string Name { get; private set; }
        public int Health { get; set; }
        public int AttackValue { get; set; }
        public char Symbol { get; set; } = '?';
        public Position Position { get; set; }
    }
}
