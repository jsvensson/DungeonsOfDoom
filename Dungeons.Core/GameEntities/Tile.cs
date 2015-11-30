using System;

namespace Dungeons.Core
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
            protected set { symbol = value; }

        }
        public override ConsoleColor Color
        {
            get { return GetTileColor(); }
            protected set { color = value; }

        }

        public Item Item { get; set; }
        public Monster Monster { get; set; }
        public virtual bool IsWalkable { get; set; }
        public bool IsNotWalkable => !IsWalkable;

        private char GetTileSymbol()
        {
            return HasItems ? Item.Symbol : symbol;
        }

        private ConsoleColor GetTileColor()
        {
            return HasItems ? Item.Color : color;
        }
    }
}
