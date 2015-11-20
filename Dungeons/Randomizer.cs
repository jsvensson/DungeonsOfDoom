using System;

namespace Dungeons
{
    static class Randomizer
    {
        static Random random = new Random();

        public static int Next(int min, int max)
        {
            return random.Next(min, max);
        }

        public static int Next(int max)
        {
            return Next(0, max);
        }

        public static bool Percentage(int chance)
        {
            return Next(101) <= chance;
        }

        public static bool Percentage(int chance, out int result)
        {
            result = Next(101);
            return result <= chance;
        }

        public static int Between(int min, int max)
        {
            return Next(min, max + 1);
        }

    }
}
