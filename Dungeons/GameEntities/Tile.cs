using System;

namespace Dungeons
{
    abstract class Tile : GameEntity
    {
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

        public Item Item { get; set; }
        public Monster Monster { get; set; }
        public virtual bool IsWalkable { get; set; }
        public bool IsNotWalkable
        {
            get { return !IsWalkable; }
        }
    }
}
