using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    class Player : Creature
    {
        const int MinimumNameLength = 3;

        public Player(string name, int health, int attackValue):
            base(name, health, attackValue, '@', ConsoleColor.Yellow)
        {
            if (!ValidateName(name))
                throw new ArgumentException("Invalid parameter", nameof(name));
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

        public static bool ValidateName(string name)
        {
            return name.Length >= MinimumNameLength;
        }
    }
}
