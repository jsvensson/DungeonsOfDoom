using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    class Player : Creature
    {
        public Player(string name, int health, int attackValue):
            base(name, health, attackValue, '@', ConsoleColor.Yellow)
        {
            if (name.Length <= 2)
                throw new ArgumentException("parameter 'name' too short");
        }

        public List<ILootable> Inventory { get; set; } = new List<ILootable>();

        public int Encumbrance
        {
            get
            {
                int weight = 0;
                foreach (ILootable item in Inventory)
                {
                    weight += item.Weight;
                }
                return weight;
            }
        }
    }
}
