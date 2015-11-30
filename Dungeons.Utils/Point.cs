namespace Dungeons
{
    public struct Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }

        public static bool Compare(Point left, Point right)
        {
            return (left.X == right.X) && (left.Y == right.Y);
        }
    }
}
