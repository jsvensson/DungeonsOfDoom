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
                Console.Clear();
                DisplayPlayerInfo();
                AskForCommand();
                player.Health--;
            } while (player.Health > 0);
        }

        void AskForCommand()
        {
            Console.Write("Enter your movement: ");
            ConsoleKeyInfo key = Console.ReadKey(false);

            // Todo: Flytta spelaren genom att modifiera dess X/Y
            // Todo: kollision
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
            // TODO: Läs in spelarens namn
            // TODO: Slumpa fram stats
            player = new Player("Bengt", 100);
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
