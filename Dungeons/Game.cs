﻿using System;
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

            player.Position = GetRandomPosition();
            WriteStatus("You have entered a dark place. You are likely to be eaten by a grue.");
            do
            {
                DrawGame();
                CheckForItems();
                AskForCommand();
                player.Health--;
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
                Position levelPos = GetRandomPosition();
                level[levelPos.X, levelPos.Y].Item = sword;
            }
        }

        void CreateMonsters()
        {
            for (int i = 0; i < 10; i++)
            {
                Troll troll = new Troll("Troll", 15, 4);
                Position pos = GetRandomPosition();
                troll.Position = pos;
                creatures.Add(troll);
            }
        }

        void AskForCommand()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (player.Position.Y > 0)
                    {
                        bool canWalk = true;
                        foreach (Creature c in creatures)
                        {
                            Position newPos = new Position(player.Position.X, player.Position.Y - 1);
                            if (Position.Compare(c.Position, newPos))
                            {
                                WriteStatus($"{c.Name} already stands there!");
                                canWalk = false;
                            }
                        }
                        if (canWalk)
                        {
                            player.Position = new Position(player.Position.X, player.Position.Y - 1);
                        }
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (player.Position.Y < worldHeight - 1)
                    {
                        bool canWalk = true;
                        foreach (Creature c in creatures)
                        {
                            Position newPos = new Position(player.Position.X, player.Position.Y + 1);
                            if (Position.Compare(c.Position, newPos))
                            {
                                WriteStatus($"{c.Name} already stands there!");
                                canWalk = false;
                            }
                        }
                        if (canWalk)
                        {
                            player.Position = new Position(player.Position.X, player.Position.Y + 1);
                        }
                    }
                    break;

                case ConsoleKey.LeftArrow:
                    if (player.Position.X > 0)
                    {
                        bool canWalk = true;
                        foreach (Creature c in creatures)
                        {
                            Position newPos = new Position(player.Position.X - 1, player.Position.Y);
                            if (Position.Compare(c.Position, newPos))
                            {
                                WriteStatus($"{c.Name} already stands there!");
                                canWalk = false;
                            }
                        }
                        if (canWalk)
                        {
                            player.Position = new Position(player.Position.X - 1, player.Position.Y);
                        }
                    }
                    break;

                case ConsoleKey.RightArrow:
                    if (player.Position.X < worldWidth - 1)
                    {
                        bool canWalk = true;
                        foreach (Creature c in creatures)
                        {
                            Position newPos = new Position(player.Position.X + 1, player.Position.Y);
                            if (Position.Compare(c.Position, newPos))
                            {
                                WriteStatus($"{c.Name} already stands there!");
                                canWalk = false;
                            }
                        }
                        if (canWalk)
                        {
                            player.Position = new Position(player.Position.X + 1, player.Position.Y);
                        }
                    }
                    break;

                case ConsoleKey.P:
                    PickupItem();
                    break;
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

            // Draw player last
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
                    level[x, y] = new Tile(random.Next(100 + 1));
                }
            }
        }

        private void CreatePlayer()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            int attack = random.Next(6) + 5;

            player = new Player(name, 100, attack);
            creatures.Add(player);
        }

        private Position GetRandomPosition()
        {
            int x = random.Next(worldWidth);
            int y = random.Next(worldHeight);

            return new Position(x, y);
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
