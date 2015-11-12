using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    abstract class Creature : GameEntity
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

        public bool Move(Direction dir, Tile[,] map)
        {
            int newX;
            int newY;

            switch (dir)
            {
                case Direction.North:
                    newX = Position.X;
                    newY = Position.Y - 1;
                    break;
                case Direction.South:
                    newX = Position.X;
                    newY = Position.Y + 1;
                    break;
                case Direction.East:
                    newX = Position.X + 1;
                    newY = Position.Y;
                    break;
                case Direction.West:
                    newX = Position.X - 1;
                    newY = Position.Y;
                    break;
                default:
                    return false;
            }
            if (map[newX, newY].HasMonster)
            {
                return false;
            }
            else
            {
                Position = new Point(newX, newY);
                return true;
            }
        }

        public string Name { get; private set; }
        public int Health { get; set; }
        public int AttackValue { get; set; }
        public Point Position { get; set; }
    }
}
