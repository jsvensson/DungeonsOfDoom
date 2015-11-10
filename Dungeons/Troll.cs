using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    class Troll : Monster
    {
        public Troll(string name, int health, int attackValue) : base(name, health, attackValue, 'T')
        {
        }
    }
}
