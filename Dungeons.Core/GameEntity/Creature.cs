using System;

namespace Dungeons.Core
{
    abstract class Creature : GameEntity, ILootable
    {
        public Creature(string name, int health, int attackValue, char symbol, ConsoleColor color):
            base(symbol, color)
        {
            Name = name;
            Health = health;
            AttackValue = attackValue;
        }

        public virtual int Fight(Creature opponent)
        {
            opponent.Health -= AttackValue;
            return opponent.Health;
        }

        public void Kill()
        {
            Symbol = '%';
            Name = $"A dead {Name}";
        }

        public MoveInfo TryToMove(Direction direction, Tile[,] level)
        {
            int newX = Position.X;
            int newY = Position.Y;
            int maxX = level.GetUpperBound(0);
            int maxY = level.GetUpperBound(1);

            switch (direction)
            {
                case Direction.North:
                    newY = Position.Y - 1;
                    break;
                case Direction.South:
                    newY = Position.Y + 1;
                    break;
                case Direction.West:
                    newX = Position.X - 1;
                    break;
                case Direction.East:
                    newX = Position.X + 1;
                    break;
                case Direction.NorthWest:
                    newX = Position.X - 1;
                    newY = Position.Y - 1;
                    break;
                case Direction.NorthEast:
                    newX = Position.X + 1;
                    newY = Position.Y - 1;
                    break;
                case Direction.SouthWest:
                    newX = Position.X - 1;
                    newY = Position.Y + 1;
                    break;
                case Direction.SouthEast:
                    newX = Position.X - 1;
                    newY = Position.Y + 1;
                    break;
            }

            // Check level bounds so we don't try to walk off the level
            if (newX < 0 || newX > maxX ||
                newY < 0 || newY > maxY)
            {
                return MoveInfo.OutOfBounds;
            }
            else if (level[newX, newY].HasMonster)
            {
                return MoveInfo.Occupied;
            }
            else if(level[newX, newY] is Wall)
            {
                return MoveInfo.BlockedByWall;
            }
            else
            {
                Point newPos = new Point(newX, newY);
                Position = newPos;
                return MoveInfo.Success;
            }
        }

        public string Name { get; set; }
        public int Weight { get; set; }
        public int Health { get; set; }
        public int AttackValue { get; set; }
        public Point Position { get; set; }
    }
}
