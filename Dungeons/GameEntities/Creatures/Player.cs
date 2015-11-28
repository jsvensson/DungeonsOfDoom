﻿using System;
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

        public List<Item> Inventory { get; set; } = new List<Item>();

        public int Encumbrance
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
