using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    // Den enda klassen som får använda I/O, tex Console.WriteLine()
    class Game
    {
        int worldWidth;
        int worldHeight;
        Tile[,] level;
        Random random = new Random();
        Player player;
        string lastStatus;
        List<Creature> creatures;

        public Game(int worldWidth, int worldHeight)
        {
            this.worldWidth = worldWidth;
            this.worldHeight = worldHeight -1;
            creatures = new List<Creature>();
        }

        public void Start()
        {
            CreateRooms();
            CreatePlayer();
            CreateMonsters();
            CreateItems();

            // Hide cursor at game start
            Console.CursorVisible = false;

            player.Position = GetRandomWalkablePosition();
            WriteStatus("You have entered a dark place. You are likely to be eaten by a grue.");
            do
            {
                DrawGame();
                AskForCommand();
                CheckForItems();
                //player.Health--;
            } while (player.Health > 0);
        }

        private bool CheckForItems()
        {
            Tile tile = level[player.Position.X, player.Position.Y];
            if (tile.HasItems)
            {
                Item item = tile.Item;
                WriteStatus($"A {item.Name} lies here.");
                return true;
            }
            else
                return false;
        }

        void CreateItems()
        {
            for (int i = 0; i < 10; i++)
            {
                Item sword = new Item("Sword", 5, '/', ConsoleColor.White);
                Point levelPos = GetRandomPosition();
                level[levelPos.X, levelPos.Y].Item = sword;
            }
        }

        void CreateMonsters()
        {
            for (int i = 0; i < 10; i++)
            {
                Troll t = new Troll("Troll", 15, 8);
                Point pos = GetRandomWalkablePosition();
                t.Position = pos;
                level[pos.X, pos.Y].Monster = t;
                creatures.Add(t);
            }

            for (int i = 0; i < 25; i++)
            {
                Goblin g = new Goblin("Cowardly Goblin", 10, 3);
                Point pos = GetRandomWalkablePosition();
                g.Position = pos;
                level[pos.X, pos.Y].Monster = g;
                creatures.Add(g);
            }
        }

        void AskForCommand()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            Tile occupiedTile = null;
            MoveInfo moveResult;

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.NumPad8:
                    moveResult = player.TryToMove(Direction.North, level);
                    if (moveResult == MoveInfo.Occupied)
                    {
                        occupiedTile = level[player.Position.X, player.Position.Y - 1];
                    }
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.NumPad2:
                    moveResult = player.TryToMove(Direction.South, level);
                    if (moveResult == MoveInfo.Occupied)
                    {
                        occupiedTile = level[player.Position.X, player.Position.Y + 1];
                    }
                    break;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.NumPad4:
                    moveResult = player.TryToMove(Direction.West, level);
                    if (moveResult == MoveInfo.Occupied)
                    {
                        occupiedTile = level[player.Position.X - 1, player.Position.Y];
                    }
                    break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.NumPad6:
                    moveResult = player.TryToMove(Direction.East, level);
                    if (moveResult == MoveInfo.Occupied)
                    {
                        occupiedTile = level[player.Position.X + 1, player.Position.Y];
                    }
                    break;
                case ConsoleKey.P:
                    PickupItem();
                    break;
            }

            // Fight if we encountered a monster
            if (occupiedTile?.Monster != null)
            {
                Monster monster = occupiedTile.Monster;
                int healthLeft = player.Fight(monster);
                if (healthLeft >= 1)
                {
                    WriteStatus($"Attacked {monster.Name} for {player.AttackValue} damage! It has {monster.Health} hp left.");

                    // Monster fights back
                    monster.Fight(player);
                }
                else
                {
                    // Monster is dead
                    WriteStatus($"{monster.Name} falls over dead!");

                    // Remove from creature list
                    // TODO: remove creature list from game
                    creatures.Remove(monster);
                    // Remove from tile
                    if (occupiedTile != null)
                    {
                        occupiedTile.Monster = null;
                    }
                }

                // Check player state
                if (player.Health <= 0 && monster != null)
                {
                    WriteStatus($"You have been slain by a {monster.Name}... Rest in pieces.");
                    Console.ReadKey();
                }
            }
        }

        private void PickupItem()
        {
            Tile tile = level[player.Position.X, player.Position.Y];
            if (tile.HasItems)
            {
                Item item = tile.Item;
                player.Inventory.Add(item);
                tile.Item = null;
                WriteStatus($"You pick up a {item.Name}.");
            }
            else
                WriteStatus("You grasp at air.");
        }

        private void DrawGame()
        {
            Console.Clear();
            Console.ResetColor();
            Console.Title = $"Health: {player.Health} Inventory: {player.Inventory.Count} Weight: {player.Encumbrance} Position: [{player.Position.X},{player.Position.Y}]";
            WriteStatus(lastStatus);
            lastStatus = "";

            // Loop through all tiles and draw items
            for (int x = 0; x < worldWidth; x++)
            {
                for (int y = 0; y < worldHeight; y++)
                {
                    Tile tile = level[x, y];
                    if(tile.Item != null)
                    {
                        DrawCharAtPos(x, y, tile.Item.Symbol, tile.Item.Color);
                    }
                }
            }

            // Draw creatures after items, in case a creature stands on an item
            foreach (Creature creature in creatures)
            {
                DrawCharAtPos(creature.Position.X, creature.Position.Y, creature.Symbol, creature.Color);
            }
        }

        private void DrawCharAtPos(int x, int y, char character, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);
            Console.Write(character);
        }

        private void CreateRooms()
        {
            // Anropar konstruktorn för en array av Room
            // Skapar 2d-array för våra rum, värde null
            level = new Tile[worldWidth, worldHeight];

            // Skapa rummen
            for (int y = 0; y < worldHeight; y++)
            {
                for (int x = 0; x < worldWidth; x++)
                {
                    level[x, y] = new Floor(random.Next(100 + 1), '.', ConsoleColor.White);
                }
            }
        }

        private void CreatePlayer()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            int attack = random.Next(5, 11);

            player = new Player(name, 25, attack);
            creatures.Add(player);
        }

        private Point GetRandomPosition()
        {
            int x = random.Next(worldWidth);
            int y = random.Next(worldHeight);

            return new Point(x, y);
        }

        private Point GetRandomWalkablePosition()
        {
            Point point;
            Tile tile;
            do
            {
                point = GetRandomPosition();
                tile = level[point.X, point.Y];
            } while (tile.HasMonster || tile.IsNotWalkable);

            return point;
        }

        private void WriteStatus(string message)
        {
            Console.SetCursorPosition(0, worldHeight);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(message);
            lastStatus = message;
        }
    }
}
