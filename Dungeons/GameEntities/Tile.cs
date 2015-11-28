using System;

namespace Dungeons
{
    abstract class Tile : GameEntity
    {
        private char symbol;
        public Tile(char symbol, ConsoleColor color) :
            base(symbol, color)
        {
            IsWalkable = true;
            Item = null;
        }

        public bool HasItems
        {
            get { return Item != null; }
        }

        public bool HasMonster
        {
            get { return Monster != null; }
        }
        public override char Symbol
        {
            get
            {
                // Return correct symbol for the tile
                return GetMapSymbol();
            }
            set { symbol = value; }

        }

        public Item Item { get; set; }
        public Monster Monster { get; set; }
        public virtual bool IsWalkable { get; set; }
        public bool IsNotWalkable
        {
            get { return !IsWalkable; }
        }

        private char GetMapSymbol()
        {
            if (HasItems)
                return Item.Symbol;
            else
                return symbol;
        }
    }
}
