using System;

namespace Dungeons
{
    abstract class Tile : GameEntity
    {
        private char symbol;
        private ConsoleColor color;

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
            get { return GetTileSymbol(); }
            set { symbol = value; }

        }
        public override ConsoleColor Color
        {
            get { return GetTileColor(); }
            set { color = value; }

        }

        public Item Item { get; set; }
        public Monster Monster { get; set; }
        public virtual bool IsWalkable { get; set; }
        public bool IsNotWalkable
        {
            get { return !IsWalkable; }
        }

        private char GetTileSymbol()
        {
            if (HasItems)
                return Item.Symbol;
            else
                return symbol;
        }

        private ConsoleColor GetTileColor()
        {
            if (HasItems)
                return Item.Color;
            else
                return color;
        }
    }
}
