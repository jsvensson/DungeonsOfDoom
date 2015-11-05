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

        public Game(int worldWidth, int worldHeight)
        {
            this.worldWidth = worldWidth;
            this.worldHeight = worldHeight;
        }

        public void Start()
        {
            CreateRooms();
            CreatePlayer();
            CreateItems();

            // Hide cursor at game start
            Console.CursorVisible = false;

            player.Position = GetRandomPosition();
            
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
            if (level[player.X, player.Y].HasItems)
            {
                Item item = level[player.X, player.Y].Item;
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
                Item sword = new Item("Sword", 5, '/');
                int[] levelPos = GetRandomPosition();
                level[levelPos[0], levelPos[1]].Item = sword;
            }
        }

        void AskForCommand()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (player.Y > 0)
                        player.Y -= 1;
                    break;

                case ConsoleKey.DownArrow:
                    if (player.Y < worldHeight - 1)
                        player.Y += 1;
                    break;

                case ConsoleKey.LeftArrow:
                    if (player.X > 0)
                        player.X -= 1;
                    break;

                case ConsoleKey.RightArrow:
                    if (player.X < worldWidth - 1)
                        player.X += 1;
                    break;
            }
        }

        private void DrawGame()
        {
            Console.Clear();
            Console.Title = $"Health: {player.Health} Position: [{player.X},{player.Y}]";

            // Loop through all tiles and draw items
            for (int x = 0; x < worldWidth; x++)
            {
                for (int y = 0; y < worldHeight; y++)
                {
                    Tile tile = level[x, y];
                    if(tile.Item != null)
                    {
                        DrawCharAtPos(x, y, tile.Item.Symbol);
                    }
                }
            }

            // Draw player last
            DrawCharAtPos(player.X, player.Y, '@');
        }

        private void DrawCharAtPos(int x, int y, char character)
        {
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
        }

        private void AddItemsToRoom()
        {
            // TODO: Implementera
        }

        private void AddMonstersToRoom()
        {
            // TODO: Implementera
        }

        private int[] GetRandomPosition()
        {
            int x = random.Next(worldWidth);
            int y = random.Next(worldHeight);

            return new int[] { x, y };
        }
    }
}
