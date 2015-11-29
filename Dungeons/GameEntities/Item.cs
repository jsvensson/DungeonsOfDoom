using System;

namespace Dungeons
{
    class Item : GameEntity, ILootable
    {
        public Item(string name, int weight, char symbol, ConsoleColor color):
            base(symbol, color)
        {
            Name = name;
            Weight = weight;
        }

        public string Name { get; set; }
        public int Weight { get; set; }
    }
}
