using System;

namespace Dungeons
{
    class Level
    {
        public Level(int width, int height, int fillRate)
        {
            SetProperties(width, height);
            Create(fillRate);
        }

        public Level(int width, int height, int fillRate, int[] iterations)
        {
            SetProperties(width, height);
            Create(fillRate);

            foreach (int generation in iterations)
            {
                Iterate(generation);
            }
            PostIterate();
        }

        private void SetProperties(int width, int height)
        {
            Width = width;
            Height = height;
            Map = new Tile[width, height];
        }

        public Tile[,] Map { get; private set; }
        private int Width { get; set; }
        private int Height { get; set; }

        public void Create(int fillRate)
        {
            Map = new Tile[Width, Height];

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (Randomizer.Percentage(fillRate))
                    {
                        Map[x, y] = new Wall(0, '#', ConsoleColor.Gray);
                    }
                    else
                    {
                        Map[x, y] = new Floor('.', ConsoleColor.DarkGray);
                    }
                }
            }

            // Create edge walls
            for (int row = 0; row < Height; row++)
            {
                Map[0, row] = new Wall(0, '#', ConsoleColor.Gray);
                Map[Width - 1, row] = new Wall(0, '#', ConsoleColor.Gray);
            }
            for (int col = 0; col < Width; col++)
            {
                Map[col, 0] = new Wall(0, '#', ConsoleColor.Gray);
                Map[col, Height - 1] = new Wall(0, '#', ConsoleColor.Gray);
            }
        }

        int CountWalls(int mapX, int mapY)
        {
            int count = 0;

            for (int row = -1; row <= 1; row++)
            {
                for (int col = -1; col <= 1; col++)
                {
                    int relativeX = col + mapX;
                    int relativeY = row + mapY;
                    // Check level boundaries
                    if (relativeX < 0 || relativeX >= Width ||
                        relativeY < 0 || relativeY >= Height)
                    {
                        // Outside boundary, counts as a wall
                        count++;
                    }
                    else if (Map[relativeX, relativeY] is Wall)
                    {
                        // Within boundaries, check if wall
                        count++;
                    }
                }
            }
            return count;
        }

        int CountWalls(Point position)
        {
            return CountWalls(position.X, position.Y);
        }

        void Iterate(int neighbors)
        {
            Tile[,] nextMapGeneration = new Tile[Width, Height];

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (CountWalls(x, y) >= neighbors)
                    {
                        nextMapGeneration[x, y] = new Wall(0, '#', ConsoleColor.Gray);
                    }
                    else
                    {
                        nextMapGeneration[x, y] = new Floor('.', ConsoleColor.DarkGray);
                    }
                }
            }

            Map = nextMapGeneration;
        }

        void PostIterate()
        {
            // Add stairs to next level
            Point stairPos = GetRandomEmptyPosition();
            Map[stairPos.X, stairPos.Y] = null;
            Map[stairPos.X, stairPos.Y] = new StairDown();
        }

        public Point GetRandomPosition()
        {
            int x = Randomizer.Next(Width);
            int y = Randomizer.Next(Height);

            return new Point(x, y);
        }

        public Point GetRandomEmptyPosition()
        {
            Point point;
            Tile tile;
            do
            {
                point = GetRandomPosition();
                tile = Map[point.X, point.Y];
            } while (tile.HasMonster || tile.IsNotWalkable);

            return point;
        }
    }
}
