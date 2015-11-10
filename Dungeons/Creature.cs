using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    abstract class Creature : GameEntity
    {
        public Creature(string name, int health, int attackValue, char symbol, ConsoleColor color) : base(symbol, color)
        {
            Name = name;
            Health = health;
            AttackValue = attackValue;
        }

        public string Name { get; private set; }
        public int Health { get; set; }
        public int AttackValue { get; set; }
        public Position Position { get; set; }
    }
}
