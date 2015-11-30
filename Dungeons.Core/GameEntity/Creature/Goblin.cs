using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons.Core
{
    class Goblin : Monster
    {
        public Goblin(string name, int health, int attackValue) :
            base(name, health, attackValue, 'g', ConsoleColor.Green)
        {
        }

        public override int Fight(Creature opponent)
        {
            if (opponent.Health >= Health * 2)
            {
                Health = 0;
            }
            else
            {
                opponent.Health -= AttackValue;
            }

            return opponent.Health;
        }
    }
}
