using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    class Game
    {
        readonly int screenWidth, screenHeight;
        readonly int levelWidth, levelHeight;
        Level level;
        Player player;
        List<Creature> creatures;
        StatusQueue statusQueue;

        public Game(int width, int height)
        {
            screenWidth = width;
            screenHeight = height;
            levelWidth = width;
            levelHeight = height - 1;
            creatures = new List<Creature>();
            statusQueue = new StatusQueue(height - 1, width - 1);
        }

        public void Start()
        {
            GameSetup();

            player.Position = level.GetRandomEmptyPosition();
            DrawFullGame();
            statusQueue.Add("You have entered a dark place. You are likely to be eaten by a grue.");
            do
            {
                Blitter.Draw();
                UpdateWindowTitle();
                statusQueue.Write();
                AskForCommand();
                CheckForItems();
            } while (player.Health > 0);
        }

        void GameSetup()
        {
            // Hide cursor at game start
            Console.CursorVisible = false;

            level = new Level(levelWidth, levelHeight, 55, new int[] { 6, 4 });
            CreatePlayer();
            CreateMonsters();
            CreateItems();
        }

        private bool CheckForItems()
        {
            Tile tile = level.Map[player.Position.X, player.Position.Y];
            if (tile.HasItems)
            {
                Item item = tile.Item;
                statusQueue.Add($"A {item.Name} lies here.");
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
                Point levelPos = level.GetRandomEmptyPosition();
                level.Map[levelPos.X, levelPos.Y].Item = sword;
            }
        }

        void CreateMonsters()
        {
            for (int i = 0; i < 10; i++)
            {
                Troll t = new Troll("Troll", 15, 8);
                Point pos = level.GetRandomEmptyPosition();
                t.Position = pos;
                level.Map[pos.X, pos.Y].Monster = t;
                creatures.Add(t);
            }

            for (int i = 0; i < 25; i++)
            {
                Goblin g = new Goblin("Cowardly Goblin", 10, 3);
                Point pos = level.GetRandomEmptyPosition();
                g.Position = pos;
                level.Map[pos.X, pos.Y].Monster = g;
                creatures.Add(g);
            }
        }

        void AskForCommand()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            Tile occupiedTile = null;
            Tile oldTile = level.Map[player.Position.X, player.Position.Y];
            Point affectedTilePos = new Point();
            Point oldPos = player.Position;
            MoveInfo moveResult = MoveInfo.Success;

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.NumPad8:
                    moveResult = player.TryToMove(Direction.North, level.Map);
                    if (moveResult == MoveInfo.Occupied)
                    {
                        occupiedTile = level.Map[player.Position.X, player.Position.Y - 1];
                        affectedTilePos = new Point(player.Position.X, player.Position.Y - 1);
                    }
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.NumPad2:
                    moveResult = player.TryToMove(Direction.South, level.Map);
                    if (moveResult == MoveInfo.Occupied)
                    {
                        occupiedTile = level.Map[player.Position.X, player.Position.Y + 1];
                        affectedTilePos = new Point(player.Position.X, player.Position.Y + 1);
                    }
                    break;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.NumPad4:
                    moveResult = player.TryToMove(Direction.West, level.Map);
                    if (moveResult == MoveInfo.Occupied)
                    {
                        occupiedTile = level.Map[player.Position.X - 1, player.Position.Y];
                        affectedTilePos = new Point(player.Position.X - 1, player.Position.Y);
                    }
                    break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.NumPad6:
                    moveResult = player.TryToMove(Direction.East, level.Map);
                    if (moveResult == MoveInfo.Occupied)
                    {
                        occupiedTile = level.Map[player.Position.X + 1, player.Position.Y];
                        affectedTilePos = new Point(player.Position.X + 1, player.Position.Y);
                    }
                    break;
                case ConsoleKey.P:
                    PickupItem();
                    break;
            }

            // Bonk if we walk into a wall
            if (moveResult == MoveInfo.BlockedByWall)
            {
                statusQueue.Add("Bonk!");
            }
            // Move succeeded, add blit for tile we moved from
            else if (moveResult == MoveInfo.Success)
            {
                // Blit old tile and new player position
                Blitter.Add(new Blixel(oldPos, oldTile));
                Blitter.Add(player.Position, player);
            }
            // Fight if we encountered a monster
            else if (occupiedTile?.Monster != null)
            {
                Monster monster = occupiedTile.Monster;
                int healthLeft = player.Fight(monster);
                if (healthLeft >= 1)
                {
                    statusQueue.Add($"Attacked {monster.Name} for {player.AttackValue} damage! It has {monster.Health} hp left.");

                    // Monster fights back
                    monster.Fight(player);
                }
                else
                {
                    // Monster is dead
                    statusQueue.Add($"{monster.Name} falls over dead!");
                    monster.Kill();

                    // Add corpse to backpack
                    player.Inventory.Add(monster);

                    // Remove from creature list
                    // TODO: remove creature list from game
                    creatures.Remove(monster);
                    // Remove from tile
                    if (occupiedTile != null)
                    {
                        occupiedTile.Monster = null;
                    }

                    // Blit floor on the tile
                    Blitter.Add(new Blixel(affectedTilePos, occupiedTile));
                }

                // Check player state
                if (player.Health <= 0 && monster != null)
                {
                    statusQueue.Add($"You have been slain by a {monster.Name}... Rest in pieces.");
                    Console.ReadKey();
                }
            }
        }

        private void PickupItem()
        {
            Tile tile = level.Map[player.Position.X, player.Position.Y];
            if (tile.HasItems)
            {
                Item item = tile.Item;
                player.Inventory.Add(item);
                tile.Item = null;
                statusQueue.Add($"You pick up a {item.Name}.");
            }
            else
                statusQueue.Add("You grasp at air.");
        }

        private void DrawFullGame()
        {
            Console.Clear();
            Console.ResetColor();
            
            // Loop through all tiles and draw items
            for (int x = 0; x < levelWidth; x++)
            {
                for (int y = 0; y < levelHeight; y++)
                {
                    Point pos = new Point(x, y);
                    Tile tile = level.Map[x, y];
                    if(tile.HasMonster)
                    {
                        // Has monster - draw monster
                        Blitter.Add(pos, tile.Monster);
                    }
                    else if(tile.HasItems && !tile.HasMonster)
                    {
                        // Has item, no monster - draw item
                        Blitter.Add(pos, tile.Item);
                    }
                    else
                    {
                        // Just draw the floor tile
                        Blitter.Add(pos, tile);
                    }
                }
            }

            // Draw player last
            Blitter.Add(player.Position, player);
        }

        private void CreatePlayer()
        {
            string name;
            bool validName;
            do
            {
                Console.Write("Enter your name: ");
                name = Console.ReadLine();
                validName = Player.ValidateName(name);
                if (!validName)
                {
                    Console.WriteLine("Name too short! Minimum of 3 characters.");
                }
            } while (!validName);

            int attack = Randomizer.Between(5, 10);

            player = new Player(name, 25, attack);
            creatures.Add(player);
        }

        void UpdateWindowTitle()
        {
            Console.Title = $"Health: {player.Health} Inventory: {player.Inventory.Count} Weight: {player.Encumbrance} Position: [{player.Position.X},{player.Position.Y}]";
        }
    }
}
