using System;

namespace Dungeons
{
    class Level
    {
        Random random = new Random();

        public Level(int width, int height, int fillRate)
        {
            this.width = width;
            this.height = height;
            Map = new Tile[width, height];
            Create(fillRate);
        }
        public Tile[,] Map { get; private set; }

        public void Create(int fillRate)
        {
            Map = new Tile[width, height];
            Floor floor = new Floor(10, '.', ConsoleColor.DarkGray);
            Wall wall = new Wall(0, '#', ConsoleColor.Gray);
            // TODO: Use static Random class
            Random r = new Random();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (random.Next(100) + 1 <= fillRate)
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
            for (int row = 0; row < height; row++)
            {
                Map[0, row] = wall;
                Map[width - 1, row] = wall;
            }
            for (int col = 0; col < width; col++)
            {
                Map[col, 0] = wall;
                Map[col, height - 1] = wall;
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
                    if (posX < 0 || posX >= width ||
                        posY < 0 || posY >= height)
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

        public void Iterate(int neighbors)
        {
            Tile[,] nextMapGeneration = new Tile[width, height];

            Floor floor = new Floor(10, '.', ConsoleColor.DarkGray);
            Wall wall = new Wall(0, '#', ConsoleColor.Gray);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
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

        public Point GetRandomPosition()
        {
            int x = random.Next(Width);
            int y = random.Next(Height);

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
