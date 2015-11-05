using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    class Player
    {
        public Player(string name, int health, int attackValue)
        {
            Name = name;
            Health = health;
            AttackValue = attackValue;
            Inventory = new List<Item>();
        }

        public string Name { get; private set; }
        public int Health { get; set; }
        public int AttackValue { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    
        public int[] Position
        {
            get { return new int[] { X, Y }; }
            set
            {
                X = value[0];
                Y = value[1];
            }
        }

        public List<Item> Inventory { get; set; }

        public int InventoryWeight
        {
            get
            {
                int weight = 0;
                foreach (Item item in Inventory)
                {
                    weight += item.Weight;
                }
                return weight;
            }
        }
    }
}
