using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    class Monster : Creature
    {
        public Monster(string name, int health, int attackValue, char symbol, ConsoleColor color):
            base(name, health, attackValue, symbol, color)
        {

        }
    }
}
