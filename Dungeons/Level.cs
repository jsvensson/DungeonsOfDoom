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
            Floor floor = new Floor('.', ConsoleColor.DarkGray);
            Wall wall = new Wall(0, '#', ConsoleColor.Gray);

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (Randomizer.Percentage(fillRate))
                    {
                        Map[x, y] = wall;
                    }
                    else
                    {
                        Map[x, y] = floor;
                    }
                }
            }

            // Create edge walls
            for (int row = 0; row < Height; row++)
            {
                Map[0, row] = wall;
                Map[Width - 1, row] = wall;
            }
            for (int col = 0; col < Width; col++)
            {
                Map[col, 0] = wall;
                Map[col, Height - 1] = wall;
            }
        }

        int CountWalls(int x, int y)
        {
            int count = 0;

            for (int row = -1; row <= 1; row++)
            {
                for (int col = -1; col <= 1; col++)
                {
                    int posX = col + x;
                    int posY = row + y;
                    // Check level boundaries
                    if (posX < 0 || posX >= Width ||
                        posY < 0 || posY >= Height)
                    {
                        // Outside boundary, counts as a wall
                        count++;
                    }
                    else if (Map[posX, posY] is Wall)
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

            Floor floor = new Floor('.', ConsoleColor.DarkGray);
            Wall wall = new Wall(0, '#', ConsoleColor.Gray);

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (CountWalls(x, y) >= neighbors)
                    {
                        nextMapGeneration[x, y] = wall;
                    }
                    else
                    {
                        nextMapGeneration[x, y] = floor;
                    }
                }
            }

            Map = nextMapGeneration;
        }

        void PostIterate()
        {
            // Add stairs to next level
            Point stairPos = GetRandomPosition();
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
