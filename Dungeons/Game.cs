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
        Room[,] rooms;
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

            do
            {
                //Console.Clear();
                DisplayPlayerInfo();
                AskForCommand();
                player.Health--;
            } while (player.Health > 0);
        }

        void AskForCommand()
        {
            Console.Write("Enter your movement: ");
            ConsoleKeyInfo key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    if (player.Y == 0)
                    {
                        Console.WriteLine("Bonk!");
                    }
                    else
                    {
                        player.Y -= 1;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (player.Y == worldHeight - 1)
                    {
                        Console.WriteLine("Bonk!");
                    }
                    else
                    {
                        player.Y += 1;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (player.X == 0)
                    {
                        Console.WriteLine("Bonk!");
                    }
                    else
                    {
                        player.X -= 1;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (player.X == worldWidth - 1)
                    {
                        Console.WriteLine("Bonk!");
                    }
                    else
                    {
                        player.X += 1;
                    }
                    break;
            }


        }

        private void DisplayPlayerInfo()
        {
            Console.WriteLine($"Health: {player.Health}   Position: [{player.X},{player.Y}]");
        }

        private void CreateRooms()
        {
            // Anropar konstruktorn för en array av Room
            // Skapar 2d-array för våra rum, värde null
            rooms = new Room[worldWidth, worldHeight];

            // Skapa rummen
            for (int y = 0; y < worldHeight; y++)
            {
                for (int x = 0; x < worldWidth; x++)
                {
                    rooms[x, y] = new Room(random.Next(100 + 1));
                }
            }
        }

        private void CreatePlayer()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            int attack = random.Next(6) + 5;

            player = new Player(name, 100);
            player.AttackValue = attack;
        }

        private void AddItemsToRoom()
        {
            // TODO: Implementera
        }

        private void AddMonstersToRoom()
        {
            // TODO: Implementera
        }

    }
}
