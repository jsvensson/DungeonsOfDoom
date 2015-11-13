using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    class Ghost : Monster
    {
        public Ghost(string name, int health, int attackValue) :
            base(name, health, attackValue, '@', ConsoleColor.DarkGray)
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
